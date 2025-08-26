using System.Collections.Generic;
using _Game._Scripts.Data.Enemy;
using _Game._Scripts.Data.Player;
using _Game._Scripts.Infrastructure.Services.Factory;
using _Game._Scripts.Infrastructure.Services.PersistantProgress;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Game._Scripts.Enemy
{
    public class LootLoader : MonoBehaviour, ISavedProgressReader
    {
        private IGameFactory _factory;
        private Dictionary<string, Loot> _lootPieces = new Dictionary<string, Loot>();
        public List<LootPiece> LootPiecesToRegister = new List<LootPiece>();
        
        public void Initialize(IGameFactory factory)
        {
            _factory = factory;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            foreach (LootDictionaryPiece lootPieceData in progress.WorldData.LootData.LootPieces)
            {
                if (!_lootPieces.ContainsKey(lootPieceData.LootId))
                {
                    _lootPieces.Add(lootPieceData.LootId, lootPieceData.Loot);
                }
            }

            foreach (LootDictionaryPiece lootPieceData in progress.WorldData.LootData.LootPieces)
            {
                if (IsSavedOnThatLevel(lootPieceData))
                {
                    Loot loot = GetLootPiece(lootPieceData.LootId);
                    
                    LootPiece lootPiece = _factory.CreateLoot();
                    lootPiece.SetupLoot(loot);
                }
            }
        }

        private Loot GetLootPiece(string lootId)
        {
            if(_lootPieces.TryGetValue(lootId, out Loot loot))
                return loot;
            
            return null;
        }
        
        private bool IsSavedOnThatLevel(LootDictionaryPiece lootPieceData) => 
            GetCurrentLevel() == lootPieceData.Loot.PositionOnLevel.Level;
        
        private string GetCurrentLevel() => 
            SceneManager.GetActiveScene().name;
        
    }
}
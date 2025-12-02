using _Game.Scripts.Data;
using _Game.Scripts.Data.Loot;
using _Game.Scripts.Data.Player;
using _Game.Scripts.Logic.Common;
using _Game.Scripts.Services.PersistantProgress;
using TMPro;
using UnityEngine;

namespace _Game.Scripts.Logic.Enemy.Loot
{
    public class LootPiece: MonoBehaviour, ISavedProgress
    {
        [SerializeField] private GameObject _skull;
        [SerializeField] private GameObject _pickupFxPrefab;
        [SerializeField] private TextMeshProUGUI _lootText;
        [SerializeField] private GameObject _pickupPopup;
        
        private Data.Loot.Loot _loot;
        private WorldData _worldData;
        private bool _pickedUp;
        private readonly float _destroyDelay = 1.5f;
        private string _id;
        private bool _loadedFromProgress;

        private void Start()
        {
            if (!_loadedFromProgress)
            {
                UniqueId uniqueId = GetComponent<UniqueId>();
                uniqueId.GenerateId();
                _id = uniqueId.Id;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_pickedUp)
            {
                _pickedUp = true;
                Pickup();
            }
        }

        public void Initialize(WorldData worldData)
        {
            _worldData = worldData;
        }

        public void SetLoot(Data.Loot.Loot loot)
        {
            _loot = loot;
        }
        
        public void LoadProgress(PlayerProgress progress)
        {
            _loadedFromProgress = true;
            
            _id =  GetComponent<UniqueId>().Id;

            LootPieceData lootPieceData = progress.WorldData.LootData.LootPiecesOnScene.Dictionary[_id];
            SetLoot(lootPieceData.Loot);
            transform.position = lootPieceData.Position.AsUnityVector();
            
        }
        
        public void UpdateProgress(PlayerProgress progress)
        {
            if (_pickedUp)
                return;

            LootPieceDataDictionary lootPieceOnScene = progress.WorldData.LootData.LootPiecesOnScene;
            
            if (!lootPieceOnScene.Dictionary.ContainsKey(_id))
            {
                lootPieceOnScene.Dictionary.Add(_id, new LootPieceData(_loot, transform.position.AsVectorData()));
            }
        }

        private void Pickup()
        {
            _worldData.LootData.Collect(_loot);
            RemoveLootPieceFromSavedPieces();

            _skull.SetActive(false);
            PlayPickupFx();
            ShowText();
            Destroy(gameObject, _destroyDelay);
        }

        private void RemoveLootPieceFromSavedPieces()
        {
            LootPieceDataDictionary lootPieceOnScene = _worldData.LootData.LootPiecesOnScene;
            if (lootPieceOnScene.Dictionary.ContainsKey(_id))
                lootPieceOnScene.Dictionary.Remove(_id);
        }

        private void PlayPickupFx() => 
            Instantiate(_pickupFxPrefab, transform.position, Quaternion.identity, transform);

        private void ShowText()
        {
            _lootText.text = _loot.Value.ToString();
            _pickupPopup.SetActive(true);
        }
    }
}
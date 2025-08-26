using System;
using _Game._Scripts.Data;
using UnityEngine;
using _Game._Scripts.Data.Player;
using _Game._Scripts.Data.Enemy;
using _Game._Scripts.Infrastructure.Services.PersistantProgress;
using _Game._Scripts.Logic;
using TMPro;
using UnityEngine.SceneManagement;

namespace _Game._Scripts.Enemy
{
    public class LootPiece: MonoBehaviour, ISavedProgress
    {
        [SerializeField] private GameObject _skull;
        [SerializeField] private GameObject _pickupFxPrefab;
        [SerializeField] private TextMeshProUGUI _lootText;
        [SerializeField] private GameObject _pickupPopup;
        private string _id;

        private void Awake()
        {
            var uniqueId =  GetComponent<UniqueId>();
            uniqueId.GenerateId();
            _id = uniqueId.Id;
            Debug.Log(_id);
        }

        private Loot _loot;
        private WorldData _worldData;
        private bool picked;
        private readonly float _destroyDelay = 1.5f;
        
        
        private void OnTriggerEnter(Collider other)
        {
            Pickup();
        }

        public void Initialize(WorldData worldData)
        {
            _worldData = worldData;
        }

        public void SetLoot(Loot loot)
        {
            _loot = loot;
        }

        public void SetupLoot(Loot loot)
        {
            SetLoot(loot);
            _loot.Value = loot.Value;
            transform.position = loot.PositionOnLevel.Position.AsUnityVector();
        }

        public void LoadProgress(PlayerProgress progress)
        {
            
            
        }
        
        public void UpdateProgress(PlayerProgress progress)
        {
            if (picked)
                return;

            _loot.PositionOnLevel = new PositionOnLevel(GetCurrentLevel(), transform.position.AsVectorData());
            progress.WorldData.LootData.LootPieces.Add(new LootDictionaryPiece(_loot, _id));
        }

        private void Pickup()
        {
            if(picked)
                return;
            
            picked = true;
            _worldData.LootData.Collect(_loot);

            _skull.SetActive(false);
            PlayPickupFx();
            ShowText();
            Destroy(gameObject, _destroyDelay);
        }

        private void PlayPickupFx() => 
            Instantiate(_pickupFxPrefab, transform.position, Quaternion.identity, transform);

        private void ShowText()
        {
            _lootText.text = _loot.Value.ToString();
            _pickupPopup.SetActive(true);
        }

        private string GetCurrentLevel() => 
            SceneManager.GetActiveScene().name;
    }
}
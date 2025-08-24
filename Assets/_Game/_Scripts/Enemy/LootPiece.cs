using UnityEngine;
using _Game._Scripts.Data.Player;
using _Game._Scripts.Data.Enemy;
using TMPro;

namespace _Game._Scripts.Enemy
{
    public class LootPiece: MonoBehaviour
    {
        [SerializeField] private GameObject _skull;
        [SerializeField] private GameObject _pickupFxPrefab;
        [SerializeField] private TextMeshProUGUI _lootText;
        [SerializeField] private GameObject _pickupPopup;
        
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
    }
}
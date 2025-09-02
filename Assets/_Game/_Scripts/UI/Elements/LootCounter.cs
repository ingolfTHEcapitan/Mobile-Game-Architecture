using _Game._Scripts.Data.Player;
using TMPro;
using UnityEngine;

namespace _Game._Scripts.UI.Elements
{
    public class LootCounter: MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _counterText;
        private WorldData _worldData;

        public void Initialize(WorldData worldData)
        {
            _worldData = worldData;
            _worldData.LootData.Changed += UpdateCounter;
        }

        private void Start()
        {
            UpdateCounter();
        }

        private void UpdateCounter()
        {
            _counterText.text = _worldData.LootData.Collected.ToString();
        }
    }
}
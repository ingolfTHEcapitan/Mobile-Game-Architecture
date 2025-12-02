using _Game.Scripts.Data.Player;
using TMPro;
using UnityEngine;

namespace _Game.Scripts.UI.Elements
{
    public class LootCounter: MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _counterText;
        
        private WorldData _worldData;

        public void Construct(WorldData worldData) => 
            _worldData = worldData;

        public void Initialize() => 
            _worldData.LootData.Changed += UpdateCounter;

        private void Start() => 
            UpdateCounter();

        private void UpdateCounter() => 
            _counterText.text = _worldData.LootData.Collected.ToString();
    }
}
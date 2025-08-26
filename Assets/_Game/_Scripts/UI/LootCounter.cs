using _Game._Scripts.Data.Player;
using _Game._Scripts.Infrastructure.Services.PersistantProgress;
using TMPro;
using UnityEngine;

namespace _Game._Scripts.UI
{
    public class LootCounter: MonoBehaviour, ISavedProgress
    {
        [SerializeField] private TextMeshProUGUI _counterText;
        private WorldData _worldData;

        public void Initialize(WorldData worldData)
        {
            _worldData = worldData;
            _worldData.LootData.Changed += UpdateCounter;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _worldData.LootData.Collected = progress.WorldData.LootData.Collected;
            UpdateCounter();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.WorldData.LootData.Collected = _worldData.LootData.Collected;
        }

        private void UpdateCounter()
        {
            _counterText.text = _worldData.LootData.Collected.ToString();
        }
    }
}
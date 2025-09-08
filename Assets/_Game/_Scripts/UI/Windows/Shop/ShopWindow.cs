using _Game._Scripts.Infrastructure.Services.Ads;
using _Game._Scripts.Infrastructure.Services.PersistantProgress;
using TMPro;
using UnityEngine;

namespace _Game._Scripts.UI.Windows.Shop
{
    public class ShopWindow: WindowBase
    {
        [SerializeField] private TextMeshProUGUI _CurrencyText;
        [SerializeField] private RewardedAdItem _rewardedAdItem;
        
        public void Inject(IAdsService adsService, IPersistantProgressService progressService)
        {
            base.Inject(progressService);
            _rewardedAdItem.Inject(adsService, progressService);
        }
        
        protected override void Initialize()
        {
            _rewardedAdItem.Initialize();
            UpdateCurrencyText();
        }

        protected override void SubscribeUpdates()
        {
            _rewardedAdItem.SubscribeUpdates();
            Progress.WorldData.LootData.Changed += UpdateCurrencyText;
        }

        protected override void UnSubscribe()
        {
            base.UnSubscribe();
            _rewardedAdItem.UnSubscribe();
            Progress.WorldData.LootData.Changed -= UpdateCurrencyText;
        }
        
        private void UpdateCurrencyText() => 
            _CurrencyText.text = Progress.WorldData.LootData.Collected.ToString();
    }
}
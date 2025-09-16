using _Game._Scripts.Infrastructure.Services.Ads;
using _Game._Scripts.Infrastructure.Services.AssetManagement;
using _Game._Scripts.Infrastructure.Services.IAP;
using _Game._Scripts.Infrastructure.Services.PersistantProgress;
using TMPro;
using UnityEngine;

namespace _Game._Scripts.UI.Windows.Shop
{
    public class ShopWindow: WindowBase
    {
        [SerializeField] private TextMeshProUGUI _CurrencyText;
        [SerializeField] private RewardedAdItem _rewardedAdItem;
        [SerializeField] private ShopItemsContainer _shopItemsContainer;
        
        public void Inject(IAdsService adsService, IPersistantProgressService progressService, 
            IIAPService iapService, IAssetProvider asset)
        {
            base.Inject(progressService);
            _rewardedAdItem.Inject(adsService, progressService);
            _shopItemsContainer.Inject(iapService, progressService, asset);
        }
        
        protected override void Initialize()
        {
            _rewardedAdItem.Initialize();
            _shopItemsContainer.Initialize();
            UpdateCurrencyText();
        }

        protected override void SubscribeUpdates()
        {
            _rewardedAdItem.SubscribeUpdates();
            _shopItemsContainer.SubscribeUpdates();
            Progress.WorldData.LootData.Changed += UpdateCurrencyText;
        }

        protected override void UnSubscribe()
        {
            base.UnSubscribe();
            _rewardedAdItem.UnSubscribe();
            _shopItemsContainer.UnSubscribe();
            Progress.WorldData.LootData.Changed -= UpdateCurrencyText;
        }
        
        private void UpdateCurrencyText() => 
            _CurrencyText.text = Progress.WorldData.LootData.Collected.ToString();
    }
}
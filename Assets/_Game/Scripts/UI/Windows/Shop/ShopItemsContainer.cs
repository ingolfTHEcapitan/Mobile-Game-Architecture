using System.Collections.Generic;
using System.Threading.Tasks;
using _Game.Scripts.Services.AssetManagement;
using _Game.Scripts.Services.IAP;
using _Game.Scripts.Services.IAP.Confings;
using _Game.Scripts.Services.PersistantProgress;
using UnityEngine;

namespace _Game.Scripts.UI.Windows.Shop
{
    public class ShopItemsContainer: MonoBehaviour
    {
        [SerializeField] private GameObject[] ShopUnavailableObjects;
        [SerializeField] private Transform _parent;
        
        private IIAPService _iapService;
        private IPersistantProgressService _progressService;
        private IAssetProvider _asset;
        
        private readonly List<GameObject> _shopItemObjects = new List<GameObject>();

        public void Construct(IIAPService iapService, IPersistantProgressService progressService, IAssetProvider asset)
        {
            _iapService = iapService;
            _progressService = progressService;
            _asset = asset;
        }

        public void Initialize() => 
            RefreshAvailableShopItems();

        public void SubscribeUpdates()
        {
            _iapService.Initialized += RefreshAvailableShopItems;
            _progressService.Progress.PurchaseData.Changed += RefreshAvailableShopItems;
        }

        public void UnSubscribe()
        {
            _iapService.Initialized -= RefreshAvailableShopItems;
            _progressService.Progress.PurchaseData.Changed -= RefreshAvailableShopItems;
        }

        private async void RefreshAvailableShopItems()
        {
            UpdateShopUnavailableObjects();

            if (!_iapService.IsInitialized)
                return;
            
            ClearShopItems();
            await FillShopItems();
        }

        private async Task FillShopItems()
        {
            foreach (ProductDescription productDescription in _iapService.GetProducts())
            {
                GameObject shopItemObject = await _asset.Instantiate(AssetAddress.ShopItem, _parent);
                ShopItem shopItem = shopItemObject.GetComponent<ShopItem>();
                
                _shopItemObjects.Add(shopItemObject);
                shopItem.Construct(_iapService, _asset, productDescription);
                await shopItem.Initialize();
            }
        }

        private void UpdateShopUnavailableObjects()
        {
            foreach (GameObject shopUnavailableObject in ShopUnavailableObjects) 
                shopUnavailableObject.SetActive(!_iapService.IsInitialized);
        }

        private void ClearShopItems()
        {
            foreach (GameObject shopItemObject in _shopItemObjects)
                Destroy(shopItemObject);
        }
    }
}
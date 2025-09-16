using System.Collections.Generic;
using _Game._Scripts.Infrastructure.Services.AssetManagement;
using _Game._Scripts.Infrastructure.Services.IAP;
using _Game._Scripts.Infrastructure.Services.IAP.Confings;
using _Game._Scripts.Infrastructure.Services.PersistantProgress;
using UnityEngine;

namespace _Game._Scripts.UI.Windows.Shop
{
    public class ShopItemsContainer: MonoBehaviour
    {
        [SerializeField] private GameObject[] ShopUnavailableObjects;
        [SerializeField] private Transform _parent;
        
        private IIAPService _iapService;
        private IPersistantProgressService _progressService;
        private IAssetProvider _asset;
        
        private readonly List<GameObject> _shopItemObjects = new List<GameObject>();

        public void Inject(IIAPService iapService, IPersistantProgressService progressService, IAssetProvider asset)
        {
            _iapService = iapService;
            _progressService = progressService;
            _asset = asset;
        }

        public void Initialize()
        {
            RefreshAvailableShopItems();
        }

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

        private void RefreshAvailableShopItems()
        {
            UpdateShopUnavailableObjects();

            if (!_iapService.IsInitialized)
                return;
            
            ClearShopItems();
            FillShopItems();
        }

        private void FillShopItems()
        {
            foreach (ProductDescription productDescription in _iapService.GetProducts())
            {
                GameObject shopItemObject = _asset.Instantiate(AssetPath.ShopItem,_parent);
                ShopItem shopItem = shopItemObject.GetComponent<ShopItem>();
                
                _shopItemObjects.Add(shopItemObject);
                shopItem.Inject(_iapService, _asset, productDescription);
                shopItem.Initialize();
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
using System;
using System.Collections.Generic;
using System.Linq;
using _Game._Scripts.Data.IAP;
using _Game._Scripts.Infrastructure.Services.IAP.Confings;
using _Game._Scripts.Infrastructure.Services.PersistantProgress;
using UnityEngine.Purchasing;

namespace _Game._Scripts.Infrastructure.Services.IAP
{
    public class IAPService : IIAPService
    {
        private readonly IAPProvider _iapProvider;
        private readonly IPersistantProgressService _progressService;

        public bool IsInitialized => _iapProvider.IsInitialized;
        
        public event Action Initialized;
        
        public IAPService(IAPProvider iapProvider, IPersistantProgressService progressService)
        {
            _iapProvider = iapProvider;
            _progressService = progressService;
        }

        public void Initialize()
        {
            _iapProvider.Initialize(iapService: this);
            _iapProvider.Initialized += () => Initialized?.Invoke();
        }

        public void StartPurchase(string productId) => 
            _iapProvider.StartPurchase(productId);

        public List<ProductDescription> GetProducts() =>
            GetProductDescriptions().ToList();

        public PurchaseProcessingResult ProcessPurchase(Product purchasedProduct)
        {
            ProductConfig productConfig = _iapProvider._productConfigs[purchasedProduct.definition.id];
             
            switch (productConfig.ItemType)
            {
                case ItemType.Skulls:
                    _progressService.Progress.WorldData.LootData.Add(productConfig.Quantity);
                    _progressService.Progress.PurchaseData.AddPurchase(purchasedProduct.definition.id);
                    break;
            }
            
            return PurchaseProcessingResult.Complete;
        }

        private IEnumerable<ProductDescription> GetProductDescriptions()
        {
            PurchaseData purchaseData = _progressService.Progress.PurchaseData;
            
            foreach (string productId in _iapProvider._products.Keys)
            {
                ProductConfig config = _iapProvider._productConfigs[productId];
                Product product = _iapProvider._products[productId];
                
                BoughtIAP boughtIap = purchaseData.BoughtIAPs.Find(x=> x.IAPId == productId);

                if (IsProductBoughtOut(boughtIap, config))
                    continue;

                yield return new ProductDescription
                {
                    Id = productId,
                    Product = product,
                    Config = config,
                    AvailablePurchasesLeft = boughtIap != null 
                        ? config.MaxPurchaseCount - boughtIap.Count 
                        : config.MaxPurchaseCount
                };
            }
        }

        private bool IsProductBoughtOut(BoughtIAP boughtIap, ProductConfig config) => 
            boughtIap != null && boughtIap.Count >= config.MaxPurchaseCount;
    }
}
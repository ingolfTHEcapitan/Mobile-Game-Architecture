using System;
using System.Collections.Generic;
using System.Linq;
using _Game._Scripts.Data;
using _Game._Scripts.Infrastructure.Services.IAP.Confings;
using UnityEngine;
using UnityEngine.Purchasing;

namespace _Game._Scripts.Infrastructure.Services.IAP
{
    public class IAPProvider: IStoreListener
    {
        private const string IAPConfigsPath = "IAP/products";
        
        private IStoreController _controller;
        private IExtensionProvider _extensions;
        private IAPService _iapService;
        
        public Dictionary<string, ProductConfig> _productConfigs { get; private set; }
        public Dictionary<string, Product> _products { get; private set; }
        
        public event Action Initialized;
        
        public bool IsInitialized => _controller != null && _extensions != null;

        public void Initialize(IAPService iapService)
        {
            _iapService = iapService;
            _productConfigs = new Dictionary<string, ProductConfig>();
            _products = new Dictionary<string, Product>();
            
            LoadProductConfigs();
            
            ConfigurationBuilder builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
            AddProducts(builder);
            
            UnityPurchasing.Initialize(this, builder);
        }

        public void StartPurchase(string productId) => 
            _controller.InitiatePurchase(productId);


        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            _controller = controller;
            _extensions = extensions;

            foreach (Product product in controller.products.all) 
                _products.Add(product.definition.id, product);
            
            Initialized?.Invoke();
            
            Debug.Log("UnityPurchasing initialization success");
        }
        
        public void OnInitializeFailed(InitializationFailureReason error) => 
            Debug.LogError($"UnityPurchasing OnInitializeFailed: {error}");

        public void OnInitializeFailed(InitializationFailureReason error, string message) => 
            Debug.LogError($"UnityPurchasing OnInitializeFailed: {error}, message: {message}");

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
        {
            Debug.Log($"UnityPurchasing ProcessPurchase success: {purchaseEvent.purchasedProduct.definition.id}");
            
            return _iapService.ProcessPurchase(purchaseEvent.purchasedProduct);
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            Debug.LogError($"product {product.definition.id} purchase failed, PurchaseFailureReason: {failureReason}," +
                           $" transaction id: {product.transactionID}");
        }

        private void AddProducts(ConfigurationBuilder builder)
        {
            foreach (var config in _productConfigs.Values) 
                builder.AddProduct(config.Id, config.ProductType);
        }

        private void LoadProductConfigs()
        {
            string configsJson = Resources.Load<TextAsset>(IAPConfigsPath).text;
            ProductConfigWrapper productConfig = configsJson.ToDeserialized<ProductConfigWrapper>();
            _productConfigs = productConfig.Configs.ToDictionary(x=> x.Id, x=> x);
        }
    }
}
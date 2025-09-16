using System;
using System.Collections.Generic;
using _Game._Scripts.Infrastructure.Services.IAP.Confings;
using UnityEngine.Purchasing;

namespace _Game._Scripts.Infrastructure.Services.IAP
{
    public interface IIAPService: IService
    {   
        bool IsInitialized { get; }
        event Action Initialized;
        void Initialize();
        void StartPurchase(string productId);
        List<ProductDescription> GetProducts();
    }
}
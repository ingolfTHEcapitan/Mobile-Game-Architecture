using System;
using System.Collections.Generic;
using _Game.Scripts.Services.IAP.Confings;

namespace _Game.Scripts.Services.IAP
{
    public interface IIAPService: IService
    {
        event Action Initialized;
        bool IsInitialized { get; }
        void Initialize();
        void StartPurchase(string productId);
        List<ProductDescription> GetProducts();
    }
}
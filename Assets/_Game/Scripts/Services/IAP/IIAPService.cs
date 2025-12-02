using System;
using System.Collections.Generic;
using _Game.Scripts.Services.IAP.Confings;

namespace _Game.Scripts.Services.IAP
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
using System;
using UnityEngine.Purchasing;

namespace _Game._Scripts.Infrastructure.Services.IAP.Confings
{
    [Serializable]
    public class ProductConfig
    {
        public string Id;
        public ProductType ProductType;
        public int MaxPurchaseCount;
        public ItemType ItemType;
        public int Quantity;
        public string Price;
        public string IconPath;
    }
}
using UnityEngine.Purchasing;
using UnityEngine.UIElements;

namespace _Game._Scripts.Infrastructure.Services.IAP.Confings
{
    public class ProductDescription
    {
        public string Id;
        public Product Product;
        public ProductConfig Config;
        public int AvailablePurchasesLeft;
    }
}
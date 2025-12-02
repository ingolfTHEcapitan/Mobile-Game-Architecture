using UnityEngine.Purchasing;

namespace _Game.Scripts.Services.IAP.Confings
{
    public class ProductDescription
    {
        public string Id;
        public Product Product;
        public ProductConfig Config;
        public int AvailablePurchasesLeft;
    }
}
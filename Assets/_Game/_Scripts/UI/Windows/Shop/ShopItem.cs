using _Game._Scripts.Infrastructure.Services.AssetManagement;
using _Game._Scripts.Infrastructure.Services.IAP;
using _Game._Scripts.Infrastructure.Services.IAP.Confings;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _Game._Scripts.UI.Windows.Shop
{
    public class ShopItem: MonoBehaviour
    {
        [SerializeField] private Button _byItemButton;
        [SerializeField] private TextMeshProUGUI _priceText;
        [SerializeField] private TextMeshProUGUI _quantityText;
        [SerializeField] private TextMeshProUGUI _availableItemsLeftText;
        [SerializeField] private Image _icon;
        
        private IIAPService _iapService;
        private IAssetProvider _asset;
        private ProductDescription _productDescription;

        public void Inject(IIAPService iapService, IAssetProvider asset, ProductDescription productDescription)
        {
            _iapService = iapService;
            _productDescription = productDescription;
            _asset = asset;
        }

        public void Initialize()
        {
            _byItemButton.onClick.AddListener(OnByItemButtonClick);
            
            FillShopItem();
        }

        private void OnByItemButtonClick() => 
            _iapService.StartPurchase(_productDescription.Id);

        private void FillShopItem()
        {
            _priceText.text = _productDescription.Config.Price;
            _quantityText.text = _productDescription.Config.Quantity.ToString();
            _availableItemsLeftText.text = $"Left {_productDescription.AvailablePurchasesLeft}";
            _icon.sprite = _asset.Load<Sprite>(_productDescription.Config.IconPath);
        }
    }
}
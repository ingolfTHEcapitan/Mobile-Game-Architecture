using System.Threading.Tasks;
using _Game.Scripts.Services.AssetManagement;
using _Game.Scripts.Services.IAP;
using _Game.Scripts.Services.IAP.Confings;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.UI.Windows.Shop
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

        public void Construct(IIAPService iapService, IAssetProvider asset, ProductDescription productDescription)
        {
            _iapService = iapService;
            _productDescription = productDescription;
            _asset = asset;
        }

        public async Task Initialize()
        {
            _byItemButton.onClick.AddListener(OnByItemButtonClick);
            await FillShopItem();
        }

        private void OnByItemButtonClick() => 
            _iapService.StartPurchase(_productDescription.Id);

        private async Task FillShopItem()
        {
            _priceText.text = _productDescription.Config.Price;
            _quantityText.text = _productDescription.Config.Quantity.ToString();
            _availableItemsLeftText.text = $"Left {_productDescription.AvailablePurchasesLeft}";
            _icon.sprite = await _asset.LoadAsync<Sprite>(_productDescription.Config.IconPath);
        }
    }
}
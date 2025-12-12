using System.Threading.Tasks;
using _Game.Scripts.Services.Ads;
using _Game.Scripts.Services.AssetManagement;
using _Game.Scripts.Services.IAP;
using _Game.Scripts.Services.PersistantProgress;
using _Game.Scripts.Services.StaticData;
using _Game.Scripts.StaticData.Windows;
using _Game.Scripts.UI.Services.Windows;
using _Game.Scripts.UI.Windows.Shop;
using UnityEngine;

namespace _Game.Scripts.UI.Services.Factory
{
    public class UIFactory: IUIFactory
    {
        private readonly IAssetProvider _asset;
        private readonly IStaticDataService _staticData;
        private readonly IPersistantProgressService _progressService;
        private readonly IAdsService _adsService;
        private readonly IIAPService _iapService;

        private GameObject _ui;
        private GameObject _popUpLayer;

        public UIFactory(IAssetProvider asset, IStaticDataService staticData,
            IPersistantProgressService progressService, IAdsService adsService, IIAPService iapService)
        {
            _progressService = progressService;
            _asset = asset;
            _staticData = staticData;
            _adsService = adsService;
            _iapService = iapService;
        }

        public void CreateShop()
        {
            WindowConfig config = _staticData.ForWindow(WindowId.Shop);
            ShopWindow shopWindow = Object.Instantiate(config.Prefab, parent: _popUpLayer.transform) as ShopWindow;
            shopWindow.Construct(_adsService, _progressService, _iapService, _asset);
        }

        public async Task CreatePopUpLayer()
        {
            _ui = GameObject.FindWithTag(Tags.UI);
            _popUpLayer = await _asset.Instantiate(AssetAddress.PopUpLayer, _ui.transform);
        }
    }
}
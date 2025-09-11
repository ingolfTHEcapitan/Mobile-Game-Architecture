using _Game._Scripts.Data;
using _Game._Scripts.Infrastructure.Services;
using _Game._Scripts.Infrastructure.Services.Ads;
using _Game._Scripts.Infrastructure.Services.AssetManagement;
using _Game._Scripts.Infrastructure.Services.PersistantProgress;
using _Game._Scripts.Infrastructure.Services.StaticData;
using _Game._Scripts.StaticData.Windows;
using _Game._Scripts.UI.Services.Windows;
using _Game._Scripts.UI.Windows.Shop;
using UnityEngine;

namespace _Game._Scripts.UI.Services.Factory
{
    public interface IUIFactory : IService
    {
        void CreateShop();
        void CreatePopUpLayer();
    }

    public class UIFactory : IUIFactory
    {
        private readonly IAssetProvider _asset;
        private readonly IStaticDataService _staticData;
        private readonly IPersistantProgressService _progressService;
        private readonly IAdsService _adsService;

        private GameObject _ui;
        private Transform _popUpLayer;

        public UIFactory(IAssetProvider asset, IStaticDataService staticData,
            IPersistantProgressService progressService, IAdsService adsService)
        {
            _progressService = progressService;
            _asset = asset;
            _staticData = staticData;
            _adsService = adsService;
        }

        public void CreateShop()
        {
            WindowConfig config = _staticData.ForWindow(WindowId.Shop);
            ShopWindow shopWindow = Object.Instantiate(config.Prefab, parent: _popUpLayer) as ShopWindow;
            shopWindow.Inject(_adsService, _progressService);
        }

        public void CreatePopUpLayer()
        {
            _ui = GameObject.FindWithTag(Tags.UI);
            _popUpLayer = _asset.Instantiate(AssetPath.PopUpLayer).SetParent(_ui).transform;
        }
    }
}
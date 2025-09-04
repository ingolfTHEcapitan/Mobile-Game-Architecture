using _Game._Scripts.Data;
using _Game._Scripts.Infrastructure.Services;
using _Game._Scripts.Infrastructure.Services.AssetManagement;
using _Game._Scripts.Infrastructure.Services.PersistantProgress;
using _Game._Scripts.Infrastructure.Services.StaticData;
using _Game._Scripts.StaticData.Windows;
using _Game._Scripts.UI.Services.Windows;
using _Game._Scripts.UI.Windows;
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

        private GameObject _ui;
        private Transform _popUpLayer;

        public UIFactory(IAssetProvider asset, IStaticDataService staticData, IPersistantProgressService progressService)
        {
            _progressService = progressService;
            _asset = asset;
            _staticData = staticData;
        }

        public void CreateShop()
        {
            WindowConfig config = _staticData.ForWindow(WindowId.Shop);
            WindowBase window = Object.Instantiate(config.Prefab, parent: _popUpLayer);
            window.Inject(_progressService);
        }

        public void CreatePopUpLayer()
        {
            _ui = GameObject.FindWithTag(SceneTag.UI);
            _popUpLayer = _asset.Instantiate(AssetPath.PopUpLayer).SetParent(_ui).transform;
        }
    }
}
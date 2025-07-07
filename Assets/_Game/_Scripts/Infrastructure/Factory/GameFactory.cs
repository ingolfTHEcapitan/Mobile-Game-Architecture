using _Game._Scripts.Infrastructure.AssetManagement;
using UnityEngine;

namespace _Game._Scripts.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assets;

        public GameFactory(IAssetProvider assets)
        {
            _assets = assets;
        }
        public GameObject CreateHero(GameObject at)
        {
            return _assets.Instantiate(AssetPath.HeroPath, at.transform.position);
        }

        public void CreateHud()
        {
            _assets.Instantiate(AssetPath.HudPath);
        }
    }
}
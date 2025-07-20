using System;
using System.Collections.Generic;
using System.Threading;
using _Game._Scripts.Infrastructure.AssetManagement;
using _Game._Scripts.Infrastructure.Services.PersistantProgress;
using UnityEngine;

namespace _Game._Scripts.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assets;

        public List<ISavedProgressReader> ProgressReaders   {get;} =  new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters   {get;} =  new List<ISavedProgress>();
        
        public GameObject HeroInstance { get; private set; }
        public event Action HeroCreated;

        public GameFactory(IAssetProvider assets)
        {
            _assets = assets;
        }

        public GameObject CreateHero(GameObject at)
        {
            HeroInstance = InstantiateRegistered(AssetPath.HeroPath, at.transform.position);
            HeroCreated?.Invoke();
            return HeroInstance;
        }

        public void CreateHud()
        {
            InstantiateRegistered(AssetPath.HudPath);
        }

        public void Cleanup()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }

        private GameObject InstantiateRegistered(string prefabPath, Vector3 at)
        {
            GameObject gameObject = _assets.Instantiate(prefabPath, at);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        private GameObject InstantiateRegistered(string prefabPath)
        {
            GameObject gameObject = _assets.Instantiate(prefabPath);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        private void RegisterProgressWatchers(GameObject gameObject)
        {
            foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
                Register(progressReader);
        }

        private void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progressWriters)
                ProgressWriters.Add(progressWriters);
            
            ProgressReaders.Add(progressReader);
        }
    }
}
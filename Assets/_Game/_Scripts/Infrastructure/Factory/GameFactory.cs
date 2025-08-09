using System;
using System.Collections.Generic;
using _Game._Scripts.Data;
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

        public GameObject CreateHero(GameObject at, GameObject parent)
        {
            HeroInstance = InstantiateRegistered(AssetPath.HeroPath, at.transform.position);
            HeroInstance.SetParent(parent);
            HeroCreated?.Invoke();
            return HeroInstance;
        }

        public GameObject CreateHud(GameObject parent)
        {
            return InstantiateRegistered(AssetPath.HudPath).SetParent(parent);
        }

        public void CleanupProgressReadersWriters()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }

        public void RegisterProgressReaders(GameObject gameObject)
        {
            foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
                RegisterProgressWriters(progressReader);
        }

        public void RegisterProgressWriters(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progressWriters)
                ProgressWriters.Add(progressWriters);
            
            ProgressReaders.Add(progressReader);
        }

        private GameObject InstantiateRegistered(string prefabPath, Vector3 at)
        {
            GameObject gameObject = _assets.Instantiate(prefabPath, at);
            RegisterProgressReaders(gameObject);
            return gameObject;
        }

        private GameObject InstantiateRegistered(string prefabPath)
        {
            GameObject gameObject = _assets.Instantiate(prefabPath);
            RegisterProgressReaders(gameObject);
            return gameObject;
        }
    }
}
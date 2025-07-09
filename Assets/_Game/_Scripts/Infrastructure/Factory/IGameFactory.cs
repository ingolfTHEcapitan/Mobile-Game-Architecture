using System.Collections.Generic;
using _Game._Scripts.Infrastructure.Services;
using _Game._Scripts.Infrastructure.Services.PersistantProgress;
using UnityEngine;

namespace _Game._Scripts.Infrastructure.Factory
{
    public interface IGameFactory: IService
    {
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }
        
        GameObject CreateHero(GameObject at);
        void CreateHud();
        void Cleanup();
    }
}
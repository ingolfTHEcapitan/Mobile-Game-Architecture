using System;
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
        GameObject HeroInstance { get; }
        event Action HeroCreated;
        
        GameObject CreateHero(GameObject at);
        void CreateHud();
        void Cleanup();
    }
}
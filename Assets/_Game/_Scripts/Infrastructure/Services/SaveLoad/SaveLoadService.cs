using _Game._Scripts.Data;
using _Game._Scripts.Infrastructure.Factory;
using _Game._Scripts.Infrastructure.Services.PersistantProgress;
using UnityEngine;

namespace _Game._Scripts.Infrastructure.Services.SaveLoad
{
    internal class SaveLoadService: ISaveLoadService
    {
        private readonly IPersistantProgressService _progressService;
        private readonly IGameFactory _gameFactory;
        private const string PlayerProgressKey = "PlayerProgress";

        public SaveLoadService(IPersistantProgressService progressService, IGameFactory gameFactory)
        {
            _progressService = progressService;
            _gameFactory = gameFactory;
        }
        
        public void SaveProgress()
        {
            foreach (ISavedProgress progressWriter in _gameFactory.ProgressWriters)
            {
                progressWriter.UpdateProgress(_progressService.Progress);
            }
            
            PlayerPrefs.SetString(PlayerProgressKey, _progressService.Progress.ToJson());
        }

        public PlayerProgress LoadProgress()
        {
             return PlayerPrefs.GetString(PlayerProgressKey)?.ToDeserialized<PlayerProgress>();
        }
    }
}
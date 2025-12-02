using _Game.Scripts.Data;
using _Game.Scripts.Data.Player;
using _Game.Scripts.Services.Factory;
using _Game.Scripts.Services.PersistantProgress;
using UnityEngine;

namespace _Game.Scripts.Services.SaveLoad
{
    internal class SaveLoadService: ISaveLoadService
    {
        private const string PlayerProgressKey = "PlayerProgress";
        
        private readonly IPersistantProgressService _progressService;
        private readonly IGameFactory _gameFactory;

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

        public PlayerProgress LoadProgress() => 
            PlayerPrefs.GetString(PlayerProgressKey)?.ToDeserialized<PlayerProgress>();
    }
}
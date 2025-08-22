using _Game._Scripts.Data.Player;
using _Game._Scripts.Infrastructure.Services.PersistantProgress;
using _Game._Scripts.Infrastructure.Services.SaveLoad;

namespace _Game._Scripts.Infrastructure.States.GameStates
{
    public class LoadProgressState : IState
    {
        private const string LevelOne = "LevelOne";
        
        private readonly GameStateMachine _stateMachine;
        private readonly IPersistantProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;

        public LoadProgressState(GameStateMachine stateMachine, IPersistantProgressService progressService, ISaveLoadService saveLoadService)
        {
            _stateMachine = stateMachine;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();
            _stateMachine.Enter<LoadLevelState, string>(_progressService.Progress.WorldData.PositionOnLevel.Level);
            
        }

        public void Exit()
        {
        }

        private void LoadProgressOrInitNew()
        {
            _progressService.Progress = _saveLoadService.LoadProgress() ?? NewProgress();
        }

        private PlayerProgress NewProgress()
        {
            PlayerProgress progress =  new PlayerProgress(initialLevel: LevelOne);
            
            progress.HeroState.MaxHealth = 100;
            progress.HeroState.ResetHealth();

            progress.heroStats.Damage = 25f;
            progress.heroStats.AttackDistance = 1f;
            progress.heroStats.AttackRadius = 0.5f;
            
            return progress;
        }
    }
}
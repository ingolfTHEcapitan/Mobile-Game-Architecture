using _Game.Scripts.Infrastructure.States;
using _Game.Scripts.Infrastructure.States.GameStates;
using _Game.Scripts.Services.AssetManagement;
using UnityEngine;

namespace _Game.Scripts.Logic.Triggers
{
    public class LevelTransferTrigger: TriggerBase
    {
        [SerializeField] private string _transferTo;

        private IStateMachine _gameStateMachine;
        private bool _triggered;
        
        public void Initialize(IStateMachine gameStateMachine) => 
            _gameStateMachine = gameStateMachine;

        private void OnTriggerEnter(Collider other)
        {
            if (_triggered)
                return;
            
            if (other.CompareTag(Tags.Player))
            {
                _gameStateMachine.Enter<LoadLevelState, string>(_transferTo);
                _triggered = true;
            }
        }
    }
}
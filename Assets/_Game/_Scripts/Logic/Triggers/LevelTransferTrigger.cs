using _Game._Scripts.Infrastructure.Services;
using _Game._Scripts.Infrastructure.Services.AssetManagement;
using _Game._Scripts.Infrastructure.States;
using _Game._Scripts.Infrastructure.States.GameStates;
using UnityEngine;

namespace _Game._Scripts.Logic.Triggers
{
    public class LevelTransferTrigger: TriggerBase
    {
        [SerializeField] private string _transferTo;

        private IStateMachine _gameStateMachine;
        private bool _triggered;
        
        private void Start() => 
            _gameStateMachine = AllServices.Container.Single<IStateMachine>();

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
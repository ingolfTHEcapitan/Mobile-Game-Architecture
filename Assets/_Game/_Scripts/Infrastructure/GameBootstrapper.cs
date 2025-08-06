using _Game._Scripts.Infrastructure.States;
using _Game._Scripts.Logic;
using UnityEngine;

namespace _Game._Scripts.Infrastructure
{
    public class GameBootstrapper: MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private LoadingCurtain _curtainPrefab;
        
        private Game _game;

        public void Awake()
        {
            _game = new Game(coroutineRunner: this, Instantiate(_curtainPrefab));
            _game.StateMachine.Enter<BootstrapState>();
            
            DontDestroyOnLoad(gameObject);
        }
    }
};
using _Game.Scripts.Infrastructure.States.GameStates;
using _Game.Scripts.UI.Elements;
using UnityEngine;

namespace _Game.Scripts.Infrastructure
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
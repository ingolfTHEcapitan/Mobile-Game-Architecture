using UnityEngine;

namespace _Game._Scripts.Infrastructure
{
    public class GameBootstrapper: MonoBehaviour, ICoroutineRunner
    {
        private Game _game;

        public void Awake()
        {
            _game = new Game(this);
            _game.StateMachine.Enter<BootstrapState>();
            
            DontDestroyOnLoad(gameObject);
        }
    }
};
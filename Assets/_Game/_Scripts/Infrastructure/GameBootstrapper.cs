using UnityEngine;

namespace _Game._Scripts.Infrastructure
{
    public class GameBootstrapper: MonoBehaviour
    {
        private Game _game;

        public void Awake()
        {
            _game = new Game();
            _game.StateMachine.Enter<BootstrapState>();
            
            DontDestroyOnLoad(gameObject);
        }
    }
};
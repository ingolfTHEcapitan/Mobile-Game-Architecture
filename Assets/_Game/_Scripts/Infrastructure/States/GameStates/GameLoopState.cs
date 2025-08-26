using _Game._Scripts.Enemy;
using _Game._Scripts.Infrastructure.Services.Factory;
using UnityEngine;

namespace _Game._Scripts.Infrastructure.States.GameStates
{
    public class GameLoopState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly IGameFactory _gameFactory;
        private const string LootLoaderTag = "LootLoader";
        public GameLoopState(GameStateMachine stateMachine, IGameFactory gameFactory)
        {
            _stateMachine = stateMachine;
            _gameFactory = gameFactory;
        }
        
        public void Enter()
        {
            LootLoader lootLoader = GameObject.FindGameObjectWithTag(LootLoaderTag).GetComponent<LootLoader>();

            foreach (LootPiece lootPieceToRegister in lootLoader.LootPiecesToRegister)
            {
                _gameFactory.RegisterProgressWriters(lootPieceToRegister);
            }
        }
        
        public void Exit()
        {
            
        }
    }
}
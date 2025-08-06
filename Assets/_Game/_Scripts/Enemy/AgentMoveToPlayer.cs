using _Game._Scripts.Infrastructure.Factory;
using _Game._Scripts.Infrastructure.Services;
using UnityEngine;
using UnityEngine.AI;

namespace _Game._Scripts.Enemy
{
    public class AgentMoveToPlayer: Follow
    {
        private const float MinimalDistance = 1f;
        
        [SerializeField] private NavMeshAgent _agent;
        
        private Transform _heroTransform;
        private IGameFactory _gameFactory;
        
        private void Start()
        {
            _gameFactory = AllServices.Container.Single<IGameFactory>();

            if (HeroExist()) 
                InitializeHeroTransform();
            else
            {
                _gameFactory.HeroCreated += InitializeHeroTransform;
            }
        }

        private void Update()
        {
            if (HeroInitialised() && HeroNotReached()) 
                _agent.destination = _heroTransform.position;
        }

        private bool HeroExist() => 
            _gameFactory.HeroInstance is not null;

        private void InitializeHeroTransform() => 
            _heroTransform = _gameFactory.HeroInstance.transform;

        private bool HeroInitialised() => 
            _heroTransform is not null;


        private bool HeroNotReached()
        {
            return Vector3.Distance(_agent.transform.position, _heroTransform.position) >= MinimalDistance;
        }
    }
}
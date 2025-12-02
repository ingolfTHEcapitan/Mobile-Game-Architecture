using UnityEngine;
using UnityEngine.AI;

namespace _Game.Scripts.Logic.Enemy.Movement
{
    public class AgentMoveToPlayer: Follow
    {
        private const float MinimalDistance = 1f;
        
        [SerializeField] private NavMeshAgent _agent;
        
        private Transform _heroTransform;

        public void Initialize(Transform heroTransform) => 
            _heroTransform = heroTransform;

        private void Update()
        {
            if (HeroInitialised() && HeroNotReached())
                _agent.destination = _heroTransform.position;
        }

        private bool HeroInitialised() => 
            _heroTransform is not null;
        
        private bool HeroNotReached() => 
            Vector3.Distance(_agent.transform.position, _heroTransform.position) >= MinimalDistance;
    }
}
using UnityEngine;
using UnityEngine.AI;

namespace _Game._Scripts.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(EnemyAnimator))]
    public class AnimateAlongAgent : MonoBehaviour
    {
        private const float MinimalVelocity = 0.01f;
        
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private EnemyAnimator _lichAnimator;

        private void Update()
        {
            if(ShouldMove())
                _lichAnimator.Move(_agent.velocity.magnitude);
            else
                _lichAnimator.StopMoving();
        }

        private bool ShouldMove()
        {
            return _agent.velocity.magnitude > MinimalVelocity && _agent.remainingDistance > _agent.radius;
        }

    }
}
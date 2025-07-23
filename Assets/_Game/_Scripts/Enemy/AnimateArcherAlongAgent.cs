using UnityEngine;

namespace _Game._Scripts.Enemy
{
    public class AnimateArcherAlongAgent: MonoBehaviour
    {
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private ArcherAnimator _archerAnimator;
        
        private bool _hasAgroTarget;

        private void Start()
        {
            _triggerObserver.TriggerEnter += OnTriggerEnter;
            _triggerObserver.TriggerExit += OnTriggerExit;
        }

        private void OnTriggerEnter(Collider obj)
        {
            if (!_hasAgroTarget)
            {
                _hasAgroTarget = true;
                _archerAnimator.PlayLoadBow();
            }
        }

        private void OnTriggerExit(Collider obj)
        {
            if (_hasAgroTarget)
            {
                _hasAgroTarget = false;
                _archerAnimator.PlayNoTarget();
            }
        }
    }
}
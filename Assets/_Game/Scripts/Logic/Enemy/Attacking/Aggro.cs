using System.Collections;
using _Game.Scripts.Logic.Common;
using _Game.Scripts.Logic.Enemy.Movement;
using UnityEngine;

namespace _Game.Scripts.Logic.Enemy.Attacking
{
    public class Aggro: MonoBehaviour
    {
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private Follow _follow;
        [SerializeField] private float _cooldown;
        
        private Coroutine _agroCoroutine;
        private bool _hasAgroTarget;

        private void Start()
        {
            _triggerObserver.TriggerEnter += OnTriggerEnter;
            _triggerObserver.TriggerExit += OnTriggerExit;

            SwitchFollowOff();
        }

        private void OnTriggerEnter(Collider obj)
        {
            if (!_hasAgroTarget)
            {
                _hasAgroTarget = true;
                
                StopAgroCoroutine();
                SwitchFollowOn();
            }
        }

        private void OnTriggerExit(Collider obj)
        {
            if (_hasAgroTarget)
            {
                _hasAgroTarget = false;
                
                _agroCoroutine = StartCoroutine(SwitchFollowOffAfterCooldown());
            }
        }

        private void StopAgroCoroutine()
        {
            if (_agroCoroutine != null)
            {
                StopCoroutine(_agroCoroutine);
                _agroCoroutine = null;
            }
        }

        private IEnumerator SwitchFollowOffAfterCooldown()
        {
            yield return new WaitForSeconds(_cooldown);
            SwitchFollowOff();
        }

        private void SwitchFollowOff() => 
            _follow.enabled = false;

        private void SwitchFollowOn() => 
            _follow.enabled = true;
    }
}
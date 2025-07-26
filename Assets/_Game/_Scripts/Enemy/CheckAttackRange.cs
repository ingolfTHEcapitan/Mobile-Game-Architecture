using System;
using Unity.VisualScripting;
using UnityEngine;

namespace _Game._Scripts.Enemy
{
    [RequireComponent(typeof(Attack))]
    public class CheckAttackRange: MonoBehaviour
    {
        [SerializeField] private Attack _attack;
        [SerializeField] private TriggerObserver _triggerObserver;

        private void Start()
        {
            _triggerObserver.TriggerEnter += OnTriggerEnter;
            _triggerObserver.TriggerExit += OnTriggerExit;
            
            _attack.DisableAttack();
        }

        private void OnTriggerEnter(Collider obj)
        {
            _attack.EnableAttack();
        }

        private void OnTriggerExit(Collider obj)
        {
            _attack.DisableAttack();
        }
    }
}
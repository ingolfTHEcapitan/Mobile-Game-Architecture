using _Game.Scripts.Logic.Common;
using UnityEngine;

namespace _Game.Scripts.Logic.Enemy.Attacking
{
    [RequireComponent(typeof(EnemyAttack))]
    public class CheckAttackRange: MonoBehaviour
    {
        [SerializeField] private EnemyAttack _enemyAttack;
        [SerializeField] private TriggerObserver _triggerObserver;

        private void Start()
        {
            _triggerObserver.TriggerEnter += OnTriggerEnter;
            _triggerObserver.TriggerExit += OnTriggerExit;
            
            _enemyAttack.DisableAttack();
        }

        private void OnTriggerEnter(Collider obj) => 
            _enemyAttack.EnableAttack();

        private void OnTriggerExit(Collider obj) => 
            _enemyAttack.DisableAttack();
    }
}
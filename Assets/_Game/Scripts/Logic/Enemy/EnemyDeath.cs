using System;
using System.Collections;
using _Game.Scripts.Logic.Enemy.Animation;
using UnityEngine;
using UnityEngine.AI;

namespace _Game.Scripts.Logic.Enemy
{
    public class EnemyDeath: MonoBehaviour
    {
        private const float DestroyDelay = 2f;

        public event Action Died;
        
        [SerializeField] private EnemyAnimator _animator;
        [SerializeField] private EnemyHealth _health;
        [SerializeField] private GameObject _deathEffect;
        [SerializeField] private NavMeshAgent _agent;
        
        private void Start() => 
            _health.HealthChanged += OnHealthChanged;

        private void OnDestroy() => 
            _health.HealthChanged -= OnHealthChanged;

        private void OnHealthChanged()
        {
            if(_health.Current <= 0)
                Die();
        }
        
        private void Die()
        {
            _health.HealthChanged -= OnHealthChanged;
            _agent.speed = 0f;
            _animator.PlayDeath();
            
            SpawnDeathEffect();
            StartCoroutine(DestroyTimer());
            
            Died?.Invoke();
        }

        private IEnumerator DestroyTimer()
        {
            yield return new WaitForSeconds(DestroyDelay);
            Destroy(gameObject);
        }

        private void SpawnDeathEffect() => 
            Instantiate(_deathEffect, transform.position, Quaternion.identity);
    }
}
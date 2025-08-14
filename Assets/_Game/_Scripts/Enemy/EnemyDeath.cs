using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace _Game._Scripts.Enemy
{
    public class EnemyDeath: MonoBehaviour
    {
        [SerializeField] private EnemyAnimator _animator;
        [SerializeField] private EnemyHealth _health;
        [SerializeField] private GameObject _deathEffect;
        [SerializeField] private NavMeshAgent _agent;
        
        private float _destroyDelay = 2f;

        public event Action Died;

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

        private void SpawnDeathEffect() => 
            Instantiate(_deathEffect, transform.position, Quaternion.identity);

        private IEnumerator DestroyTimer()
        {
            yield return new WaitForSeconds(_destroyDelay);
            Destroy(gameObject);
        }
    }
}
using System;
using _Game._Scripts.Logic;
using UnityEngine;

namespace _Game._Scripts.Enemy
{
    public class EnemyHealth : MonoBehaviour, IHealth
    {
        [SerializeField] private LichAnimator _lichAnimator;
        [SerializeField] private float _current;
        [SerializeField] private float _max;

        public float Current
        {
            get => _current;
            set => _current = value;
        }

        public float Max
        {
            get => _max;
            set => _max = value;
        }
        
        public event Action HealthChanged;

        public void TakeDamage(float damage)
        {
            Current -= damage;
            
            _lichAnimator.PlayHit();
            
            HealthChanged?.Invoke();
        }
    }
}
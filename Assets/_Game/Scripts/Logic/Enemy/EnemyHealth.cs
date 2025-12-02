using System;
using _Game.Scripts.Logic.Common;
using _Game.Scripts.Logic.Enemy.Animation;
using UnityEngine;

namespace _Game.Scripts.Logic.Enemy
{
    public class EnemyHealth : MonoBehaviour, IHealth
    {
        [SerializeField] private EnemyAnimator _animator;
        [SerializeField] private float _current;
        [SerializeField] private float _max;

        public float Current
        {
            get => _current;
            set => _current = Mathf.Clamp(value, 0, Max);
        }

        public float Max
        {
            get => _max;
            set => _max = value;
        }
        
        public event Action HealthChanged;

        public void TakeDamage(float damage)
        {
            if (Current <= 0)
                return;
            
            Current -= damage;
            
            _animator.PlayHit();
            HealthChanged?.Invoke();
        }
    }
}
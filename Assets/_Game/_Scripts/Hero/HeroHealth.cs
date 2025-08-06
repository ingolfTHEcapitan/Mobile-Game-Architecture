using System;
using UnityEngine;
using _Game._Scripts.Data;
using _Game._Scripts.Infrastructure.Services.PersistantProgress;
using _Game._Scripts.Logic;

namespace _Game._Scripts.Hero
{
    [RequireComponent(typeof(HeroAnimator))]
    public class HeroHealth : MonoBehaviour, ISavedProgress, IHealth
    {
        [SerializeField] private HeroAnimator _heroAnimator;
        
        private HeroState _state;
        
        public event Action HealthChanged;

        public float Current
        {
            get => _state.CurrentHealth;
            set
            {
                if (_state.CurrentHealth != value)
                {
                    _state.CurrentHealth = value; 
                    HealthChanged?.Invoke();
                }
            }
        }

        public float Max
        {
            get => _state.MaxHealth;
            set => _state.MaxHealth = value;
        }

        public void TakeDamage(float damage)
        {
            if (Current <= 0)
                return;
            
            Current -= damage;
            _heroAnimator.PlayHit();

        }
        
        public void LoadProgress(PlayerProgress progress)
        {
            _state = progress.HeroState;
            HealthChanged?.Invoke();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.HeroState.CurrentHealth = Current;
            progress.HeroState.MaxHealth = Max;
        }
    }
}
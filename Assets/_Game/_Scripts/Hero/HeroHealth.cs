using _Game._Scripts.Data;
using _Game._Scripts.Infrastructure.Services.PersistantProgress;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

namespace _Game._Scripts.Hero
{
    [RequireComponent(typeof(HeroAnimator))]
    public class HeroHealth : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private HeroAnimator _heroAnimator;
        
        private HeroState _state;

        public float Current
        {
            get => _state.CurrentHealth;
            private set => _state.CurrentHealth = value;
        }

        public float Max
        {
            get => _state.MaxHealth;
            private set => _state.MaxHealth = value;
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
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.HeroState.CurrentHealth = Current;
            progress.HeroState.MaxHealth = Max;
        }
    }
}
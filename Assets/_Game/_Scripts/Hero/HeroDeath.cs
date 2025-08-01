using _Game._Scripts.Data;
using JetBrains.Annotations;
using UnityEngine;

namespace _Game._Scripts.Hero
{
    public class HeroDeath: MonoBehaviour
    {
        [SerializeField] private HeroHealth _heroHealth;
        [SerializeField] private HeroAnimator _heroAnimator;
        [SerializeField] private HeroMove _heroMove;
        [SerializeField] private GameObject _DeathEffect;
        
        private bool _isDeath;
        
        private const float _deathEffectPositionOffsetY = 1.5f;
        private const string _dynamicTag = "Dynamic";

        private void Start()
        {
            _heroHealth.HealthChanged += OnHealthChanged;
        }

        [UsedImplicitly]
        private void OnDeathAnimationEnded()
        {
            GameObject deathFX = Instantiate(_DeathEffect, GetEffectPosition(), Quaternion.identity);
            deathFX.SetParent(GameObject.FindWithTag(_dynamicTag));
        }
        
        private void OnHealthChanged()
        {
            if (!_isDeath && _heroHealth.Current <= 0) 
                Die();
        }

        private void Die()
        {
            _isDeath = true;
            
            _heroMove.enabled = false;
            _heroAnimator.PlayDeath();
        }

        private Vector3 GetEffectPosition()
        {
            return transform.position + new Vector3(0f, _deathEffectPositionOffsetY, 0f);
        }
    }
}
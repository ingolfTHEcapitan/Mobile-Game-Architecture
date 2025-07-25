using System;
using _Game._Scripts.Infrastructure.Factory;
using _Game._Scripts.Infrastructure.Services;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Game._Scripts.Enemy
{
    [RequireComponent(typeof(LichAnimator))]
    public class Attack: MonoBehaviour
    {
        [SerializeField] private LichAnimator _lichAnimator;
        [SerializeField] private float _attackCooldown = 3.0f;
        
        private IGameFactory _factory;
        private Transform _heroTransform;
        private float _currentCooldown;
        private bool _isAttacking;
        
        private void Awake()
        {
            _factory = AllServices.Container.Single<IGameFactory>();
            _factory.HeroCreated += OnHeroCreated;
        }

        private void Update()
        {
            UpdateCooldown();
            
            if (CanAttack())
                StartAttack();
        }

        public void OnAttack()
        {
              
        }

        public void OnAttackEnded()
        { 
            _currentCooldown = _attackCooldown;
            _isAttacking = false;
        }

        private void UpdateCooldown()
        {
            if (!CooldownIsUp()) 
                _currentCooldown -= Time.deltaTime;
        }

        private void StartAttack()
        {
            transform.LookAt(_heroTransform);
            _lichAnimator.PlayAttack01();
            
            _isAttacking = true;
        }

        private bool CanAttack() => 
            !_isAttacking && CooldownIsUp();

        private bool CooldownIsUp()
            => _currentCooldown <= 0.0f;

        private void OnHeroCreated() => 
            _heroTransform = _factory.HeroInstance.transform;
    }
}
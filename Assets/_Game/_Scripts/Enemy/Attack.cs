using System;
using System.Linq;
using _Game._Scripts.Hero;
using _Game._Scripts.Infrastructure.Factory;
using _Game._Scripts.Infrastructure.Services;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Game._Scripts.Enemy
{
    [RequireComponent(typeof(LichAnimator))]
    public class Attack: MonoBehaviour
    {
        [SerializeField] private LichAnimator _lichAnimator;
        [SerializeField] private float _damage = 10f;
        [SerializeField] private float _attackCooldown = 3.0f;
        [SerializeField] private float _attackRadius = 0.5f;
        [SerializeField] private float _attackDistance = 0.5f;
        [SerializeField] private float _debugLifeTime = 1.0f;
        
        private IGameFactory _factory;
        private Transform _heroTransform;
        private float _currentCooldown;
        private bool _isAttacking;
        private bool _attackIsActive;
        private Collider[] _hits = new Collider[1];
        private int _layerMask;

        private void Awake()
        {
            _factory = AllServices.Container.Single<IGameFactory>();
            _factory.HeroCreated += OnHeroCreated;
            _layerMask = LayerMask.GetMask("Player");
        }

        private void Update()
        {
            UpdateCooldown();
            
            if (CanAttack())
                StartAttack();
        }

        [UsedImplicitly]
        private void OnAttack()
        {
            if (Hit(out Collider hit))
            {
                PhysicsDebug.DrawDebugSphere(GetStartPoint(), _attackRadius, _debugLifeTime);
                
                HeroHealth heroHealth = hit.transform.GetComponent<HeroHealth>();
                heroHealth.TakeDamage(_damage);
            }
        }

        [UsedImplicitly]
        private void OnAttackEnded()
        { 
            _currentCooldown = _attackCooldown;
            _isAttacking = false;
        }

        public void EnableAttack() => 
            _attackIsActive = true;

        public void DisableAttack() => 
            _attackIsActive = false;

        private bool Hit(out Collider hit)
        {
            int hitCount = Physics.OverlapSphereNonAlloc(GetStartPoint(), _attackRadius, _hits, _layerMask);

            hit = _hits.FirstOrDefault();
            return hitCount > 0;
        }

        private Vector3 GetStartPoint() => 
            new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z) + transform.forward * _attackDistance;

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
            _attackIsActive && !_isAttacking && CooldownIsUp();

        private bool CooldownIsUp()
            => _currentCooldown <= 0.0f;

        private void OnHeroCreated() => 
            _heroTransform = _factory.HeroInstance.transform;
    }
}
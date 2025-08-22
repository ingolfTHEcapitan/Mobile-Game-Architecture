using System.Linq;
using _Game._Scripts.Infrastructure.Services.Factory;
using _Game._Scripts.Logic;
using JetBrains.Annotations;
using UnityEngine;

namespace _Game._Scripts.Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class EnemyAttack: MonoBehaviour
    {
        public float Damage = 10f;
        public float Cooldown = 3.0f;
        public float Radius = 0.5f;
        public float Distance = 0.5f;
        
        [SerializeField] private EnemyAnimator _animator;
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
            PhysicsDebug.DrawDebugSphere(GetStartPoint(), Radius, _debugLifeTime, Color.red);
            if (Hit(out Collider hit))
            {
                PhysicsDebug.DrawDebugSphere(GetStartPoint(), Radius, _debugLifeTime, Color.green);
                
                IHealth heroHealth = hit.transform.GetComponent<IHealth>();
                heroHealth.TakeDamage(Damage);
            }
        }

        [UsedImplicitly]
        private void OnAttackEnded()
        { 
            _currentCooldown = Cooldown;
            _isAttacking = false;
        }

        public void Initialize(Transform heroTransform)
        {
            _heroTransform = heroTransform;
        }

        public void EnableAttack() =>
            _attackIsActive = true;

        public void DisableAttack() => 
            _attackIsActive = false;

        private void StartAttack()
        {
            transform.LookAt(_heroTransform);
            _animator.PlayAttack01();
            
            _isAttacking = true;
        }

        private bool Hit(out Collider hit)
        {
            int hitCount = Physics.OverlapSphereNonAlloc(GetStartPoint(), Radius, _hits, _layerMask);

            hit = _hits.FirstOrDefault();
            return hitCount > 0;
        }

        private void UpdateCooldown()
        {
            if (!CooldownIsUp()) 
                _currentCooldown -= Time.deltaTime;
        }

        private Vector3 GetStartPoint() => 
            new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z) + transform.forward * Distance;

        private bool CanAttack() => 
            _attackIsActive && !_isAttacking && CooldownIsUp();

        private bool CooldownIsUp()
            => _currentCooldown <= 0.0f;
    }
}
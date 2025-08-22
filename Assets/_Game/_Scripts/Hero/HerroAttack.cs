using _Game._Scripts.Data.Player;
using _Game._Scripts.Enemy;
using _Game._Scripts.Infrastructure.Services.Input;
using _Game._Scripts.Infrastructure.Services.PersistantProgress;
using _Game._Scripts.Logic;
using JetBrains.Annotations;
using UnityEngine;

namespace _Game._Scripts.Hero
{
    public class HerroAttack : MonoBehaviour, ISavedProgressReader
    {
        [SerializeField] private HeroAnimator _heroAnimator;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private float _debugLifeTime = 1.0f;
        
        private IInputService _input;
        private Collider[] _hits = new Collider[5];
        private int _layerMask;
        private HeroStats heroStats;
        

        private void Update()
        {
            if (_input.IsAttackButtonUp() && !_heroAnimator.IsAttacking)
            {
                _heroAnimator.PlayAttack01();
            }
        }
        
        [UsedImplicitly]
        private void OnAttack()
        {
            PhysicsDebug.DrawDebugSphere(GetStartPoint(), heroStats.AttackRadius, _debugLifeTime, Color.red);
            
            if (Hit() > 0) 
                PhysicsDebug.DrawDebugSphere(GetStartPoint(), heroStats.AttackRadius, _debugLifeTime, Color.green);
            
            for (var index = 0; index < Hit(); ++index)
            {
                IHealth targetHealth = _hits[index].transform.GetComponentInParent<IHealth>();
                targetHealth.TakeDamage(heroStats.AttackDamage);
            }
        }

        public void Initialize(IInputService inputService)
        {
            _input = inputService;
            _layerMask = LayerMask.GetMask("Hittable");
        }

        public void LoadProgress(PlayerProgress progress) => 
            heroStats = progress.HeroStats;

        private int Hit() => 
            Physics.OverlapSphereNonAlloc(GetStartPoint(), heroStats.AttackRadius, _hits, _layerMask);

        private Vector3 GetStartPoint() => 
            new Vector3(transform.position.x, _characterController.center.y, transform.position.z) 
            + transform.forward * heroStats.AttackDistance;
    }
}
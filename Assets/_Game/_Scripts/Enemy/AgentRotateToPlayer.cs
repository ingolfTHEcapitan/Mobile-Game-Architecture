using _Game._Scripts.Infrastructure.Factory;
using _Game._Scripts.Infrastructure.Services;
using UnityEngine;

namespace _Game._Scripts.Enemy
{
    public class AgentRotateToPlayer: Follow
    {
        [SerializeField] private float _speed;
        
        private Transform _heroTransform;
        private Vector3 _positionToLook;
        private IGameFactory _gameFactory;
        
        private void Start()
        {
            _gameFactory = AllServices.Container.Single<IGameFactory>();

            if (HeroExist()) 
                InitializeHeroTransform();
            else
                _gameFactory.HeroCreated += InitializeHeroTransform;
        }

        private bool HeroExist() => 
            _gameFactory.HeroInstance is not null;
        
        private void InitializeHeroTransform() => 
            _heroTransform = _gameFactory.HeroInstance.transform;
        
        private void Update()
        {
            if (HeroInitialised())
                RotateTowardsHero();
        }

        private void RotateTowardsHero()
        {
            UpdatePositionToLookAt();
            transform.rotation = SmoothRotation(transform.rotation, _positionToLook);
        }

        private void UpdatePositionToLookAt()
        {
            Vector3 positionDifference = _heroTransform.position - transform.position;
            _positionToLook = new Vector3(positionDifference.x, transform.position.y, positionDifference.z);
        }

        private Quaternion SmoothRotation(Quaternion rotation, Vector3 positionToLook)
        {
            return Quaternion.Lerp(rotation, TargetRotation(positionToLook), SpeedFactor());
        }

        private Quaternion TargetRotation(Vector3 positionToLook) => 
            Quaternion.LookRotation(positionToLook);

        private float SpeedFactor() => 
            _speed * Time.deltaTime;

        private bool HeroInitialised() => 
            _heroTransform is not null;
    }
}
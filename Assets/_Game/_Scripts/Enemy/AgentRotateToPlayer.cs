using UnityEngine;

namespace _Game._Scripts.Enemy
{
    public class AgentRotateToPlayer: Follow
    {
        [SerializeField] private float _speed;
        
        private Transform _heroTransform;
        private Vector3 _positionToLook;
        
        public void Initialize(Transform heroTransform)
        {
            _heroTransform = heroTransform;
        }
        
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
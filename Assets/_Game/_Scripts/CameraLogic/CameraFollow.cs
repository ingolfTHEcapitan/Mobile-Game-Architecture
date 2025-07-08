using UnityEngine;

namespace _Game._Scripts.CameraLogic
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform _followTarget;
        [SerializeField] private float _rotationAngleX;
        [SerializeField] private float _rotationAngleY;
        [SerializeField] private float _distance;
        [SerializeField] private float _offsetY;

        private void LateUpdate()
        {
            if (_followTarget is null)
                return;
        
            Quaternion rotation = Quaternion.Euler(_rotationAngleX, _rotationAngleY, 0);
            Vector3 position = rotation * new Vector3(0, 0, -_distance) + FollowingPointPosition();
            transform.position = position;
            transform.rotation = rotation;
        }

        public void Follow(GameObject followTarget)
        {
            _followTarget = followTarget.transform;
        }
    
        private Vector3 FollowingPointPosition()
        {
            Vector3 followPosition = _followTarget.position;
            followPosition.y += _offsetY;
            return followPosition;
        
        }
    }
}

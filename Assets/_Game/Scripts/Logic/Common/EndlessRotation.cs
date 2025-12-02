using UnityEngine;

namespace _Game.Scripts.Logic.Common
{
    public class EndlessRotation : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private void Update() => 
            transform.Rotate(Vector3.up, Time.deltaTime * _speed);
    }
}
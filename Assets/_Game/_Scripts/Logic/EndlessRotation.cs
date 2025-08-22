using System;
using UnityEngine;

namespace _Game._Scripts.Logic
{
    public class EndlessRotation : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private void Update()
        {
            transform.Rotate(Vector3.up, Time.deltaTime * _speed);
        }
    }
}
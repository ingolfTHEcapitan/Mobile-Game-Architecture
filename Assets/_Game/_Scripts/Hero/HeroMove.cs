using System;
using _Game._Scripts.Infrastructure;
using _Game._Scripts.Services.Input;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace _Game._Scripts.Hero
{
    public class HeroMove : MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private float _movementSpeed = 5f;
        
        private IInputService _inputService;
        private Camera _camera;
        private Vector3 _movementVector;


        private void Awake()
        {
            _inputService = Game.InputService;
        }

        private void Start()
        {
            _camera = Camera.main; 
        }

        private void Update()
        {
            _movementVector = Vector3.zero;

            if (_inputService.Axis.sqrMagnitude > Constants.Epsilon)
            {
                _movementVector = _camera.transform.TransformDirection(_inputService.Axis);
                _movementVector.y = 0;
                _movementVector.Normalize();
                
                transform.forward = _movementVector;
            }
            
            _movementVector += Physics.gravity;
            
            _characterController.Move(_movementVector * (_movementSpeed * Time.deltaTime));
            
        }
    }
}
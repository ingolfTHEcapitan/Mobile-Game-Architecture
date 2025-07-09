using _Game._Scripts.Data;
using _Game._Scripts.Infrastructure.Services;
using _Game._Scripts.Infrastructure.Services.Input;
using _Game._Scripts.Infrastructure.Services.PersistantProgress;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Game._Scripts.Hero
{
    public class HeroMove : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private float _movementSpeed = 5f;
        
        private IInputService _inputService;
        private Camera _camera;
        private Vector3 _movementVector;
        private Vector3Data position;

        private void Awake()
        {
            _inputService = AllServices.Container.Single<IInputService>();
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

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.WorldData.PositionOnLevel =
                new PositionOnLevel(GetCurrentLevel(), transform.position.AsVectorData());
        }

        public void LoadProgress(PlayerProgress progress)
        {
            if (GetCurrentLevel() == progress.WorldData.PositionOnLevel.Level)
            {
                Vector3Data savedPosition = progress.WorldData.PositionOnLevel.Position;
                if (savedPosition != null)
                {
                    Warp(to: savedPosition);
                }
            }
        }

        private void Warp(Vector3Data to)
        {
            // Если перемещать позицию объекта из кода с прикреплённым на нём CharacterController,
            // то объект может застрять где-то или провалиться под текстуры,
            // поэтому CharacterController нужно отключить, поменять позицию и снова включить.
            _characterController.enabled = false;
            transform.position = to.AsUnityVector();
            _characterController.enabled = true;
        }

        private string GetCurrentLevel()
        {
            return SceneManager.GetActiveScene().name;
        }
    }
}
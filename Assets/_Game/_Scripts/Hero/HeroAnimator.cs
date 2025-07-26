using UnityEngine;

namespace _Game._Scripts.Hero
{
    public class HeroAnimator : MonoBehaviour
    {
        private const string Speed = "Speed";
        
        [SerializeField] private Animator _animator;
        [SerializeField] private CharacterController _characterController;
    
        private readonly int _speedHash = Animator.StringToHash(Speed);
 
        private void Update()
        {
            _animator.SetFloat(_speedHash, _characterController.velocity.magnitude);
        }

        public void PlayHit()
        {
            
        }
    }
}

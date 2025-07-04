using UnityEngine;

public class HeroAnimator : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField] CharacterController _characterController;
    
    private readonly int _speedHash = Animator.StringToHash("Speed");
 
    private void Update()
    {
        _animator.SetFloat(_speedHash, _characterController.velocity.magnitude);
    }
}

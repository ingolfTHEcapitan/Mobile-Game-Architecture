using System;
using _Game._Scripts.Logic.Animation;
using UnityEngine;

namespace _Game._Scripts.Hero
{
    public class HeroAnimator : MonoBehaviour, IAnimationStateReader
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private CharacterController _characterController;
        
        private static readonly int SpeedHash = Animator.StringToHash("Speed");
        private static readonly int Die = Animator.StringToHash("Die");
        private static readonly int Hit = Animator.StringToHash("Hit");
        private static readonly int Attack_1 = Animator.StringToHash("Attack_1");
        
        private readonly int _idleStateHash = Animator.StringToHash("Hero_Idle");
        private readonly int _dieStateHash = Animator.StringToHash("Hero_Die");
        private readonly int _getHitStateHash = Animator.StringToHash("Hero_GetHit");
        private readonly int _moveStateHash = Animator.StringToHash("Hero_Run");
        private readonly int _attack01StateHash = Animator.StringToHash("Hero_Attack01");
        
        public AnimatorState State { get; private set; }
        public bool IsAttacking => State == AnimatorState.Attack01;
        
        public event Action<AnimatorState> StateEntered;
        public event Action<AnimatorState> StateExited;
        
        private void Update()
        {
            _animator.SetFloat(SpeedHash, _characterController.velocity.magnitude);
        }

        public void PlayHit() => _animator.SetTrigger(Hit);
        public void PlayDeath() => _animator.SetTrigger(Die);
        public void PlayAttack01() => _animator.SetTrigger(Attack_1);
        
        
        public void EnteredState(int stateHash)
        {
            State = GetState(stateHash);
            StateEntered?.Invoke(State);
        }
        
        public void ExitedState(int stateHash)
        {
            State = GetState(stateHash);
            StateExited?.Invoke(State);
        }
        
        private AnimatorState GetState(int stateHash)
        {
            AnimatorState state;

            if (stateHash == _idleStateHash) 
                state = AnimatorState.Idle;
            else if (stateHash == _dieStateHash) 
                state = AnimatorState.Die;
            else if (stateHash == _getHitStateHash) 
                state = AnimatorState.GetHit;
            else if (stateHash == _moveStateHash) 
                state = AnimatorState.Move;
            else if (stateHash == _attack01StateHash) 
                state = AnimatorState.Attack01;
            else 
                state = AnimatorState.Unknown;
            
            return state;
        }
    }
}

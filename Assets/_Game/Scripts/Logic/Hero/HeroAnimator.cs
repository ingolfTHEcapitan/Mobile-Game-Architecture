using System;
using _Game.Scripts.Logic.Animation;
using UnityEngine;

namespace _Game.Scripts.Logic.Hero
{
    public class HeroAnimator : MonoBehaviour, IAnimationStateReader
    {
        public event Action<AnimatorState> StateEntered;
        public event Action<AnimatorState> StateExited;
        
        [SerializeField] private Animator _animator;
        [SerializeField] private CharacterController _characterController;

        private readonly int _speedHash = Animator.StringToHash("Speed");
        private readonly int _dieHash = Animator.StringToHash("Die");
        private readonly int _hitHash = Animator.StringToHash("Hit");
        private readonly int _attack01Hash = Animator.StringToHash("Attack_1");
        private readonly int _idleStateHash = Animator.StringToHash("Hero_Idle");
        private readonly int _dieStateHash = Animator.StringToHash("Hero_Die");
        private readonly int _getHitStateHash = Animator.StringToHash("Hero_GetHit");
        private readonly int _moveStateHash = Animator.StringToHash("Hero_Run");
        private readonly int _attack01StateHash = Animator.StringToHash("Hero_Attack01");

        public AnimatorState State { get; private set; }
        public bool IsAttacking => State == AnimatorState.Attack01;


        private void Update() => 
            _animator.SetFloat(_speedHash, _characterController.velocity.magnitude);

        public void PlayHit() => _animator.SetTrigger(_hitHash);
        public void PlayDeath() => _animator.SetTrigger(_dieHash);
        public void PlayAttack01() => _animator.SetTrigger(_attack01Hash);
        
        
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

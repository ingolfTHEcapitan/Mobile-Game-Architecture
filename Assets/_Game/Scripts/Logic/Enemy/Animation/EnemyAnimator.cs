using System;
using _Game.Scripts.Logic.Animation;
using UnityEngine;

namespace _Game.Scripts.Logic.Enemy.Animation
{
    [RequireComponent(typeof(Animator))]
    public class EnemyAnimator : MonoBehaviour, IAnimationStateReader
    {
        public event Action<AnimatorState> StateEntered;
        public event Action<AnimatorState> StateExited;
        
        private readonly int _dieHash = Animator.StringToHash("Die");
        private readonly int _winHash = Animator.StringToHash("Win");
        private readonly int _hitHash = Animator.StringToHash("Hit");
        private readonly int _attack01Hash = Animator.StringToHash("Attack_1");
        private readonly int _attack02Hash = Animator.StringToHash("Attack_2");
        private readonly int _speedHash = Animator.StringToHash("Speed");
        private readonly int _isMovingHash = Animator.StringToHash("IsMoving");
        private readonly int _idleStateHash = Animator.StringToHash("Idle");
        private readonly int _dieStateHash = Animator.StringToHash("Die");
        private readonly int _victoryStateHash = Animator.StringToHash("Victory");
        private readonly int _getHitStateHash = Animator.StringToHash("GetHit");
        private readonly int _moveStateHash = Animator.StringToHash("Move");
        private readonly int _attack01StateHash = Animator.StringToHash("Attack01");
        private readonly int _attack02StateHash = Animator.StringToHash("Attack02");

        private Animator _animator;
        
        public AnimatorState State { get; private set; }

        private void Awake() => 
            _animator = GetComponent<Animator>();

        public void PlayDeath() => 
            _animator.SetTrigger(_dieHash);
        public void PlayVictory() => 
            _animator.SetTrigger(_winHash);
        public void PlayHit() => 
            _animator.SetTrigger(_hitHash);

        public void Move(float speed)
        {
            _animator.SetBool(_isMovingHash, true);
            _animator.SetFloat(_speedHash, speed);
        }

        public void StopMoving() => 
            _animator.SetBool(_isMovingHash, false);

        public void PlayAttack01() => 
            _animator.SetTrigger(_attack01Hash);
        public void PlayAttack02() => 
            _animator.SetTrigger(_attack02Hash);

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
            else if (stateHash == _victoryStateHash)
                state = AnimatorState.Victory;
            else if (stateHash == _getHitStateHash)
                state = AnimatorState.GetHit;
            else if (stateHash == _moveStateHash)
                state = AnimatorState.Move;
            else if (stateHash == _attack01StateHash)
                state = AnimatorState.Attack01;
            else if (stateHash == _attack02StateHash)
                state = AnimatorState.Attack02;
            else
                state = AnimatorState.Unknown;

            return state;
        }
    }
}
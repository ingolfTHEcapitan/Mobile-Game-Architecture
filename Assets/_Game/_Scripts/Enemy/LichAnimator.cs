using System;
using _Game._Scripts.Logic;
using UnityEngine;

namespace _Game._Scripts.Enemy
{
    public class LichAnimator: MonoBehaviour, IAnimationStateReader
    {
        private static readonly int Die = Animator.StringToHash("Die");
        private static readonly int Win = Animator.StringToHash("Win");
        private static readonly int Hit = Animator.StringToHash("Hit");
        private static readonly int Attack_1 = Animator.StringToHash("Attack_1");
        private static readonly int Attack_2 = Animator.StringToHash("Attack_2");
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");
        
        private readonly int _idleStateHash = Animator.StringToHash("idle");
        private readonly int _dieStateHash = Animator.StringToHash("die");
        private readonly int _victoryStateHash = Animator.StringToHash("victory");
        private readonly int _getHitStateHash = Animator.StringToHash("GetHit");
        private readonly int _moveStateHash = Animator.StringToHash("Move");
        private readonly int _attack01StateHash = Animator.StringToHash("attack01");
        private readonly int _attack02StateHash = Animator.StringToHash("attack02");
        
        private Animator _animator;
        
        public event Action<AnimatorState> StateEntered;
        public event Action<AnimatorState> StateExited;
        
        public AnimatorState State { get; private set; }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void PlayDeath() => _animator.SetTrigger(Die);
        public void PlayVictory() => _animator.SetTrigger(Win);
        public void PlayHit() => _animator.SetTrigger(Hit);

        public void Move(float speed)
        {
            _animator.SetBool(IsMoving, true);
            _animator.SetFloat(Speed, speed);
        }
        
        public void StopMoving()
        {
            _animator.SetBool(IsMoving, false);
        }

        public void PlayAttack01() => _animator.SetTrigger(Attack_1);
        public void PlayAttack02() => _animator.SetTrigger(Attack_2);

        public void EnteredState(int stateHash)
        {
            State = GetState(stateHash);
            StateEntered?.Invoke(State);
        }
        
        public void ExitedState(int stateHash)
        {
            State = GetState(stateHash);
            StateEntered?.Invoke(State);
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
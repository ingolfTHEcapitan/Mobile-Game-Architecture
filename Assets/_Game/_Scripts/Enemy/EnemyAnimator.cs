using System;
using _Game._Scripts.Logic.Animation;
using UnityEngine;

namespace _Game._Scripts.Enemy
{
    [RequireComponent(typeof(Animator))]
    public class EnemyAnimator : MonoBehaviour, IAnimationStateReader
    {
        private static readonly int DieHash = Animator.StringToHash("Die");
        private static readonly int WinHash = Animator.StringToHash("Win");
        private static readonly int HitHash = Animator.StringToHash("Hit");
        private static readonly int Attack01Hash = Animator.StringToHash("Attack_1");
        private static readonly int Attack02Hash = Animator.StringToHash("Attack_2");
        private static readonly int SpeedHash = Animator.StringToHash("Speed");
        private static readonly int IsMovingHash = Animator.StringToHash("IsMoving");

        private readonly int _idleStateHash = Animator.StringToHash("Idle");
        private readonly int _dieStateHash = Animator.StringToHash("Die");
        private readonly int _victoryStateHash = Animator.StringToHash("Victory");
        private readonly int _getHitStateHash = Animator.StringToHash("GetHit");
        private readonly int _moveStateHash = Animator.StringToHash("Move");
        private readonly int _attack01StateHash = Animator.StringToHash("Attack01");
        private readonly int _attack02StateHash = Animator.StringToHash("Attack02");

        private Animator _animator;

        public event Action<AnimatorState> StateEntered;
        public event Action<AnimatorState> StateExited;

        public AnimatorState State { get; private set; }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void PlayDeath() => _animator.SetTrigger(DieHash);
        public void PlayVictory() => _animator.SetTrigger(WinHash);
        public void PlayHit() => _animator.SetTrigger(HitHash);

        public void Move(float speed)
        {
            _animator.SetBool(IsMovingHash, true);
            _animator.SetFloat(SpeedHash, speed);
        }

        public void StopMoving()
        {
            _animator.SetBool(IsMovingHash, false);
        }

        public void PlayAttack01() => _animator.SetTrigger(Attack01Hash);
        public void PlayAttack02() => _animator.SetTrigger(Attack02Hash);

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
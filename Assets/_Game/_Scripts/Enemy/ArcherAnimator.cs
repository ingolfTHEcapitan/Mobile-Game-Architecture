using System;
using _Game._Scripts.Logic.Animation;
using UnityEngine;

namespace _Game._Scripts.Enemy
{
    [RequireComponent(typeof(Animator))]
    public class ArcherAnimator : MonoBehaviour, IAnimationStateReader
    {
        private static readonly int DieHash = Animator.StringToHash("Die");
        private static readonly int LoadHash = Animator.StringToHash("Load");
        private static readonly int NoTargetHash = Animator.StringToHash("NoTarget");
        
        private readonly int _combatIdleStateHash = Animator.StringToHash("CombatIdle");
        private readonly int _deathStateHash = Animator.StringToHash("Death");
        private readonly int _bowLoadStateHash = Animator.StringToHash("BowLoad");
        private readonly int _bowHoldStateHash = Animator.StringToHash("BowHold");
        
        private Animator _animator;
        
        public event Action<AnimatorState> StateEntered;
        public event Action<AnimatorState> StateExited;
        
        public AnimatorState State { get; private set; }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void PlayDeath() => _animator.SetTrigger(DieHash);
        public void PlayLoadBow() => _animator.SetTrigger(LoadHash);
        public void PlayNoTarget() => _animator.SetTrigger(NoTargetHash);
        
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

            if (stateHash == _combatIdleStateHash) 
                state = AnimatorState.Idle;
            else if (stateHash == _deathStateHash) 
                state = AnimatorState.Die;
            else if (stateHash == _bowLoadStateHash) 
                state = AnimatorState.Load;
            else if (stateHash == _bowHoldStateHash) 
                state = AnimatorState.Hold;
            else 
                state = AnimatorState.Unknown;
            
            return state;
        }
    }
}
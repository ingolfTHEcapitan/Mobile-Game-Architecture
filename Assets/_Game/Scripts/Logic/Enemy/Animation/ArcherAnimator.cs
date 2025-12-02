using System;
using _Game.Scripts.Logic.Animation;
using UnityEngine;

namespace _Game.Scripts.Logic.Enemy.Animation
{
    [RequireComponent(typeof(Animator))]
    public class ArcherAnimator : MonoBehaviour, IAnimationStateReader
    {
        public event Action<AnimatorState> StateEntered;
        public event Action<AnimatorState> StateExited;
        
        private readonly int _dieHash = Animator.StringToHash("Die");
        private readonly int _loadHash = Animator.StringToHash("Load");
        private readonly int _noTargetHash = Animator.StringToHash("NoTarget");
        private readonly int _combatIdleStateHash = Animator.StringToHash("CombatIdle");
        private readonly int _deathStateHash = Animator.StringToHash("Death");
        private readonly int _bowLoadStateHash = Animator.StringToHash("BowLoad");
        private readonly int _bowHoldStateHash = Animator.StringToHash("BowHold");

        private Animator _animator;
        public AnimatorState State { get; private set; }

        private void Awake() => 
            _animator = GetComponent<Animator>();

        public void PlayDeath() => 
            _animator.SetTrigger(_dieHash);
        public void PlayLoadBow() => 
            _animator.SetTrigger(_loadHash);
        public void PlayNoTarget() => 
            _animator.SetTrigger(_noTargetHash);
        
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
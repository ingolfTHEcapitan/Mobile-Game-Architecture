using _Game._Scripts.Enemy;
using UnityEngine;

namespace _Game._Scripts.Logic
{
    public class AnimationStateReporter: StateMachineBehaviour
    {
        private IAnimationStateReader _stateReader;
        
        
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);
            _stateReader = FindReader(animator);
            
            _stateReader.EnteredState(stateInfo.shortNameHash);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateExit(animator, stateInfo, layerIndex);
            _stateReader = FindReader(animator);
            
            _stateReader.ExitedState(stateInfo.shortNameHash);
        }

        private IAnimationStateReader FindReader(Animator animator)
        {
            if (_stateReader is not null)
                return _stateReader;
            
            _stateReader = animator.gameObject.GetComponent<IAnimationStateReader>();
            return _stateReader;
        }
    }
}
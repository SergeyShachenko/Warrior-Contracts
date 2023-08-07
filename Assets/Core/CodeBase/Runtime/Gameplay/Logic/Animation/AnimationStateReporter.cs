using UnityEngine;

namespace WC.Runtime.Gameplay.Logic
{
  public class AnimationStateReporter : StateMachineBehaviour
  {
    private IAnimationStateReader _animationStateReader;

    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      base.OnStateEnter(animator, stateInfo, layerIndex);
      
      SetAnimationStateReader(animator);
      _animationStateReader.EnteredState(stateInfo.shortNameHash);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      base.OnStateExit(animator, stateInfo, layerIndex);
      
      SetAnimationStateReader(animator);
      _animationStateReader.ExitedState(stateInfo.shortNameHash);
    }

    private void SetAnimationStateReader(Component animator)
    {
      if (_animationStateReader != null) return;
      

      _animationStateReader = animator.gameObject.GetComponent<IAnimationStateReader>();
    }
  }
}
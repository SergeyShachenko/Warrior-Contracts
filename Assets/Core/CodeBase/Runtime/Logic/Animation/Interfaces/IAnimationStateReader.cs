using System;

namespace WC.Runtime.Logic.Animation
{
  public interface IAnimationStateReader
  {
    event Action<AnimationState> StateEnter, StateExit;
    
    AnimationState State { get; }
    
    void EnteredState(int stateHash);
    void ExitedState(int stateHash);
  }
}
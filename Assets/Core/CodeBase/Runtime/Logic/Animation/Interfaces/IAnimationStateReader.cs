namespace WC.Runtime.Logic.Animation
{
  public interface IAnimationStateReader
  {
    AnimationState State { get; }
    
    void EnteredState(int stateHash);
    void ExitedState(int stateHash);
  }
}
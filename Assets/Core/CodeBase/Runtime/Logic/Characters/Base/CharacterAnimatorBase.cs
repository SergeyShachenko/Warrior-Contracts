using System;
using UnityEngine;
using WC.Runtime.Logic.Animation;
using AnimationState = WC.Runtime.Logic.Animation.AnimationState;

namespace WC.Runtime.Logic.Characters
{
  public abstract class CharacterAnimatorBase : IAnimator, IAnimationStateReader
  {
    public event Action Changed;
    
    public event Action<AnimationState> StateEnter;
    public event Action<AnimationState> StateExit;

    public bool IsActive { get; set; } = true;
    public Animator Animator { get; }
    public AnimationState State { get; private set; }
    
    public bool IsAttacking => State == AnimationState.Attack;

    protected CharacterAnimatorBase(Animator animator) => Animator = animator;

    
    public virtual void Tick() { }
    
    
    public abstract void PlayAttack();
    public abstract void PlayHit();
    public abstract void PlayDeath();
    
    protected abstract AnimationState StateFor(int stateHash);
    
    void IAnimationStateReader.EnteredState(int stateHash)
    {
      State = StateFor(stateHash);
      StateEnter?.Invoke(State);
    }

    void IAnimationStateReader.ExitedState(int stateHash)
    {
      StateExit?.Invoke(StateFor(stateHash));
    }
  }
}
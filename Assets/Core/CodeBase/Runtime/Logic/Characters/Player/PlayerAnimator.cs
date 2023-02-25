using System;
using UnityEngine;
using WC.Runtime.Logic.Animation;
using AnimationState = WC.Runtime.Logic.Animation.AnimationState;

namespace WC.Runtime.Logic.Characters
{
  public class PlayerAnimator : ICharacterAnimator,
    IAnimationStateReader
  {
    public event Action<AnimationState> StateEnter, StateExit;

    public bool IsActive { get; set; } = true;
    public Animator Animator { get; }
    public AnimationState State { get; private set; }
    public bool IsAttacking => State == AnimationState.Attack;

    private readonly CharacterController _charController;
    private readonly CharacterAnimationObserver _animObserver;

    private static readonly int SpeedHash = Animator.StringToHash("Speed");
    private static readonly int AttackHash = Animator.StringToHash("Attack");
    private static readonly int HitHash = Animator.StringToHash("Hit");
    private static readonly int DeathHash = Animator.StringToHash("Death");

    private readonly int _idleStateHash = Animator.StringToHash("Idle");
    private readonly int _walkStateHash = Animator.StringToHash("Walk");
    private readonly int _attackStateHash = Animator.StringToHash("Attack");
    private readonly int _deathStateHash = Animator.StringToHash("Death");

    public PlayerAnimator(CharacterController charController, Animator animator)
    {
      _charController = charController;
      Animator = animator;
    }

    
    public void Tick()
    {
      if (IsActive == false) return;
      
      Animator.SetFloat(SpeedHash, _charController.velocity.magnitude, 0.1f, Time.deltaTime);
    }

    public void PlayHit()
    {
      if (IsActive == false) return;
      
      Animator.SetTrigger(HitHash);
    }

    public void PlayAttack()
    {
      if (IsActive == false) return;
      
      Animator.SetTrigger(AttackHash);
    }

    public void PlayDeath()
    {
      if (IsActive == false) return;
      
      Animator.SetTrigger(DeathHash);
    }

    public void ResetToIdle()
    {
      if (IsActive == false) return;
      
      Animator.Play(_idleStateHash, -1);
    }

    private AnimationState StateFor(int stateHash)
    {
      AnimationState state;
      
      if (stateHash == _idleStateHash)
        state = AnimationState.Idle;
      else if (stateHash == _attackStateHash)
        state = AnimationState.Attack;
      else if (stateHash == _walkStateHash)
        state = AnimationState.Walking;
      else if (stateHash == _deathStateHash)
        state = AnimationState.Died;
      else
        state = AnimationState.Unknown;
      
      return state;
    }

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
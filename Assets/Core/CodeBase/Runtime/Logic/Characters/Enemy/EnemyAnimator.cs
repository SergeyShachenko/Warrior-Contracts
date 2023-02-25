using System;
using UnityEngine;
using WC.Runtime.Logic.Animation;
using AnimationState = WC.Runtime.Logic.Animation.AnimationState;

namespace WC.Runtime.Logic.Characters
{
  public class EnemyAnimator : ICharacterAnimator,
    IAnimationStateReader
  {
    public event Action<AnimationState> StateEnter, StateExit;

    public bool IsActive { get; set; } = true;
    public AnimationState State { get; private set; }
    public bool IsAttacking => State == AnimationState.Attack;
    public Animator Animator => _animator;

    private static readonly int IsMovingHash = Animator.StringToHash("IsMoving");
    private static readonly int SpeedHash = Animator.StringToHash("Speed");
    private static readonly int AttackHash = Animator.StringToHash("Attack");
    private static readonly int HitHash = Animator.StringToHash("Hit");
    private static readonly int DeathHash = Animator.StringToHash("Death");

    private readonly int _idleStateHash = Animator.StringToHash("Idle");
    private readonly int _walkStateHash = Animator.StringToHash("Walk");
    private readonly int _attackStateHash = Animator.StringToHash("Attack");
    private readonly int _deathStateHash = Animator.StringToHash("Death");
    
    private readonly Animator _animator;

    public EnemyAnimator(Animator animator) => 
      _animator = animator;


    public void Move(float speed)
    {
      if (IsActive == false) return;
      
      _animator.SetBool(IsMovingHash, true);
      _animator.SetFloat(SpeedHash, speed);
    }

    public void StopMove()
    {
      if (IsActive == false) return;
      
      _animator.SetBool(IsMovingHash, false);
    }

    public void PlayAttack()
    {
      if (IsActive == false) return;

      _animator.SetTrigger(AttackHash);
    }

    public void PlayHit()
    {
      if (IsActive == false) return;
      
      _animator.SetTrigger(HitHash);
    }

    public void PlayDeath()
    {
      if (IsActive == false) return;
      
      _animator.SetTrigger(DeathHash);
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
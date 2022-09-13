using System;
using CodeBase.Logic.Animation;
using UnityEngine;
using AnimationState = CodeBase.Logic.Animation.AnimationState;

namespace CodeBase.Logic.Enemy
{
  public class EnemyAnimator : MonoBehaviour,
    IAnimationStateReader
  {
    public AnimationState State { get; private set; }

    public Animator Animator => _animator;
    
    public event Action<AnimationState> OnStateEnter, OnStateExit;

    [SerializeField] private Animator _animator;

    private static readonly int IsMovingHash = Animator.StringToHash("IsMoving");
    private static readonly int SpeedHash = Animator.StringToHash("Speed");
    private static readonly int AttackHash = Animator.StringToHash("Attack");
    private static readonly int HitHash = Animator.StringToHash("Hit");
    private static readonly int DeathHash = Animator.StringToHash("Death");

    private readonly int _idleStateHash = Animator.StringToHash("Idle");
    private readonly int _walkStateHash = Animator.StringToHash("Walk");
    private readonly int _attackStateHash = Animator.StringToHash("Attack");
    private readonly int _deathStateHash = Animator.StringToHash("Death");


    public void Move(float speed)
    {
      _animator.SetBool(IsMovingHash, true);
      _animator.SetFloat(SpeedHash, speed);
    }

    public void StopMove() =>
      _animator.SetBool(IsMovingHash, false);

    public void PlayAttack() =>
      _animator.SetTrigger(AttackHash);

    public void PlayHit() =>
      _animator.SetTrigger(HitHash);

    public void PlayDeath() =>
      _animator.SetTrigger(DeathHash);

    public void EnteredState(int stateHash)
    {
      State = StateFor(stateHash);
      OnStateEnter?.Invoke(State);
    }

    public void ExitedState(int stateHash) =>
      OnStateExit?.Invoke(StateFor(stateHash));

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
  }
}
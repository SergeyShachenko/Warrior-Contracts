using System;
using UnityEngine;
using WC.Runtime.Logic.Animation;
using AnimationState = WC.Runtime.Logic.Animation.AnimationState;

namespace WC.Runtime.Logic.Characters
{
  public class EnemyAnimator : MonoBehaviour,
    IAnimationStateReader
  {
    public event Action Attack, AttackEnd;
    public event Action<AnimationState> StateEnter, StateExit;
    
    public AnimationState State { get; private set; }
   
    public Animator Animator => _animator;

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
      StateEnter?.Invoke(State);
    }

    public void ExitedState(int stateHash) =>
      StateExit?.Invoke(StateFor(stateHash));

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

    private void OnAttack() => 
      Attack?.Invoke();

    private void OnAttackEnd() => 
      AttackEnd?.Invoke();
  }
}
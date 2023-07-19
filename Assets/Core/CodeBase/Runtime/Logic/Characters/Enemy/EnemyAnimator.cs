using UnityEngine;
using AnimationState = WC.Runtime.Logic.Animation.AnimationState;

namespace WC.Runtime.Logic.Characters
{
  public class EnemyAnimator : CharacterAnimatorBase
  {
    private static readonly int IsMovingHash = Animator.StringToHash("IsMoving");
    private static readonly int SpeedHash = Animator.StringToHash("Speed");
    private static readonly int AttackHash = Animator.StringToHash("Attack");
    private static readonly int HitHash = Animator.StringToHash("Hit");
    private static readonly int DeathHash = Animator.StringToHash("Death");

    private readonly int _idleStateHash = Animator.StringToHash("Idle");
    private readonly int _walkStateHash = Animator.StringToHash("Walk");
    private readonly int _attackStateHash = Animator.StringToHash("Attack");
    private readonly int _deathStateHash = Animator.StringToHash("Death");

    public EnemyAnimator(Animator animator) : base(animator) {}


    public void Move(float speed)
    {
      if (IsActive == false) return;
      
      Animator.SetBool(IsMovingHash, true);
      Animator.SetFloat(SpeedHash, speed);
    }

    public void StopMove()
    {
      if (IsActive == false) return;
      
      Animator.SetBool(IsMovingHash, false);
    }

    public override void PlayAttack()
    {
      if (IsActive == false) return;

      Animator.SetTrigger(AttackHash);
    }

    public override void PlayHit()
    {
      if (IsActive == false) return;
      
      Animator.SetTrigger(HitHash);
    }

    public override void PlayDeath()
    {
      if (IsActive == false) return;
      
      Animator.SetTrigger(DeathHash);
    }

    protected override AnimationState StateFor(int stateHash)
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
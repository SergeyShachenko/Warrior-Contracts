using UnityEngine;
using AnimationState = WC.Runtime.Logic.Animation.AnimationState;

namespace WC.Runtime.Logic.Characters
{
  public class PlayerAnimator : CharacterAnimatorBase
  {
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

    public PlayerAnimator(CharacterController charController, Animator animator) : base(animator) => 
      _charController = charController;


    public override void Tick()
    {
      if (IsActive == false) return;
      
      Animator.SetFloat(SpeedHash, _charController.velocity.magnitude, 0.1f, Time.deltaTime);
    }

    public override void PlayHit()
    {
      if (IsActive == false) return;
      
      Animator.SetTrigger(HitHash);
    }

    public override void PlayAttack()
    {
      if (IsActive == false) return;
      
      Animator.SetTrigger(AttackHash);
    }

    public override void PlayDeath()
    {
      if (IsActive == false) return;
      
      Animator.SetTrigger(DeathHash);
    }

    public void ResetToIdle()
    {
      if (IsActive == false) return;
      
      Animator.Play(_idleStateHash, -1);
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
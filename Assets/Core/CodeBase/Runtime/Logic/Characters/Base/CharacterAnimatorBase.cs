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

    protected float p_DampTime => Math.Max(_character.Movement.AccelerationTime - 0.4f, 0f);
    
    protected readonly int p_IsMovingHash = Animator.StringToHash("IsMoving");
    protected readonly int p_SpeedHash = Animator.StringToHash("Speed");
    
    protected readonly int p_HorizontalHash = Animator.StringToHash("Horizontal");
    protected readonly int p_VerticalHash = Animator.StringToHash("Vertical");
    
    protected readonly int p_SlowWalkHash = Animator.StringToHash("SlowWalk");
    protected readonly int p_RunHash = Animator.StringToHash("Run");
    protected readonly int p_AimHash = Animator.StringToHash("Aim");

    protected readonly int p_AttackHash = Animator.StringToHash("Attack");
    protected readonly int p_AttackIDHash = Animator.StringToHash("AttackID");
    protected readonly int p_HitHash = Animator.StringToHash("Hit");
    protected readonly int p_HitIDHash = Animator.StringToHash("HitID");
    protected readonly int p_DeathHash = Animator.StringToHash("Death");
    protected readonly int p_DeathIDHash = Animator.StringToHash("DeathID");
    protected readonly int p_IsDeadHash = Animator.StringToHash("IsDead");

    private readonly int _walkStateHash = Animator.StringToHash("Walk");
    private readonly int _walkSlowStateHash = Animator.StringToHash("Walk_Slow");
    private readonly int _walkAimHandsStateHash = Animator.StringToHash("Walk_Aim_Hands");
    private readonly int _runStateHash = Animator.StringToHash("Run");
    private readonly int _hitStateHash = Animator.StringToHash("Hit");
    private readonly int _attackStateHash = Animator.StringToHash("Attack");
    private readonly int _deathStateHash = Animator.StringToHash("Death");
    
    private readonly CharacterBase _character;

    protected CharacterAnimatorBase(CharacterBase character, Animator animator)
    {
      _character = character;
      Animator = animator;
    }


    public virtual void Tick() { }


    public virtual void PlayHit(int id)
    {
      if (IsActive == false) return;

      Animator.SetFloat(p_HitIDHash, id);
      Animator.SetTrigger(p_HitHash);
    }

    public virtual void PlayAttack(int id)
    {
      if (IsActive == false) return;

      Animator.SetFloat(p_AttackIDHash, id);
      Animator.SetTrigger(p_AttackHash);
    }

    public virtual void PlayDeath(int id)
    {
      if (IsActive == false) return;
      
      Animator.SetFloat(p_DeathIDHash, id);
      Animator.SetTrigger(p_DeathHash);
      Animator.SetBool(p_IsDeadHash, true);
    }
    
    void IAnimationStateReader.EnteredState(int stateHash)
    {
      State = GetState(stateHash);
      StateEnter?.Invoke(State);
    }

    void IAnimationStateReader.ExitedState(int stateHash) => StateExit?.Invoke(GetState(stateHash));

    private AnimationState GetState(int stateHash)
    {
      AnimationState state;
      
      if (stateHash == _walkStateHash) state = AnimationState.Walk;
      else if (stateHash == _walkSlowStateHash) state = AnimationState.WalkSlow;
      else if (stateHash == _walkAimHandsStateHash) state = AnimationState.WalkAimHands;
      else if (stateHash == _runStateHash) state = AnimationState.Run;
      else if (stateHash == _hitStateHash) state = AnimationState.Hit;
      else if (stateHash == _attackStateHash) state = AnimationState.Attack;
      else if (stateHash == _deathStateHash) state = AnimationState.Death;
      else state = AnimationState.Unknown;
      
      return state;
    }
  }
}
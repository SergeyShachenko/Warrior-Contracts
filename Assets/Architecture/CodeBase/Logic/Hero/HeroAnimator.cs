using System;
using CodeBase.Logic.Animation;
using UnityEngine;
using AnimationState = CodeBase.Logic.Animation.AnimationState;

namespace CodeBase.Logic.Hero
{
  public class HeroAnimator : MonoBehaviour, 
    IAnimationStateReader
  {
    public AnimationState State { get; private set; }
    
    public Animator Animator => _animator;
    public CharacterController CharacterController => _characterController;
    
    public event Action<AnimationState> OnStateEnter, OnStateExit;

    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Animator _animator;

    private static readonly int SpeedHash = Animator.StringToHash("Speed");
    private static readonly int AttackHash = Animator.StringToHash("Attack");
    private static readonly int HitHash = Animator.StringToHash("Hit");
    private static readonly int DeathHash = Animator.StringToHash("Death");
    
    private readonly int _idleStateHash = Animator.StringToHash("Idle");
    private readonly int _walkStateHash = Animator.StringToHash("Walk");
    private readonly int _attackStateHash = Animator.StringToHash("Attack");
    private readonly int _deathStateHash = Animator.StringToHash("Death");
    

    private void Update()
    {
      _animator.SetFloat(SpeedHash, _characterController.velocity.magnitude, 0.1f, Time.deltaTime);
    }
    

    public bool IsAttacking => 
      State == AnimationState.Attack;

    public void PlayHit() => 
      _animator.SetTrigger(HitHash);
    
    public void PlayAttack() => 
      _animator.SetTrigger(AttackHash);

    public void PlayDeath() =>  
      _animator.SetTrigger(DeathHash);

    public void ResetToIdle() =>
      _animator.Play(_idleStateHash, -1);
    
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
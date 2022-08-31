using System;
using CodeBase.Logic.Animation;
using UnityEngine;

namespace CodeBase.Logic.Character
{
  public class CharacterAnimator : MonoBehaviour, IAnimationStateReader
  {
    public AnimatorState State { get; private set; }
    
    public Animator Animator => _animator;
    public CharacterController CharacterController => _characterController;
    
    public event Action<AnimatorState> StateEntered, StateExited;

    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Animator _animator;

    private readonly int _movementMagnitudeHash = Animator.StringToHash("Movement Magnitude");
    private readonly int _attackHash = Animator.StringToHash("Attack");
    private readonly int _getDamageHash = Animator.StringToHash("Get Damage");
    private readonly int _deathHash = Animator.StringToHash("Death");
    
    private readonly int _idleStateHash = Animator.StringToHash("Idle");
    private readonly int _attackStateHash = Animator.StringToHash("Attack Normal");
    private readonly int _walkingStateHash = Animator.StringToHash("Run");
    private readonly int _deathStateHash = Animator.StringToHash("Die");
    

    private void Update()
    {
      _animator.SetFloat(_movementMagnitudeHash, _characterController.velocity.magnitude, 0.1f, Time.deltaTime);
    }
    

    public bool IsAttacking => 
      State == AnimatorState.Attack;

    public void PlayHit() => 
      _animator.SetTrigger(_getDamageHash);
    
    public void PlayAttack() => 
      _animator.SetTrigger(_attackHash);

    public void PlayDeath() =>  
      _animator.SetTrigger(_deathHash);

    public void ResetToIdle() =>
      _animator.Play(_idleStateHash, -1);
    
    public void EnteredState(int stateHash)
    {
      State = StateFor(stateHash);
      StateEntered?.Invoke(State);
    }

    public void ExitedState(int stateHash) =>
      StateExited?.Invoke(StateFor(stateHash));
    
    private AnimatorState StateFor(int stateHash)
    {
      AnimatorState state;
      if (stateHash == _idleStateHash)
        state = AnimatorState.Idle;
      else if (stateHash == _attackStateHash)
        state = AnimatorState.Attack;
      else if (stateHash == _walkingStateHash)
        state = AnimatorState.Walking;
      else if (stateHash == _deathStateHash)
        state = AnimatorState.Died;
      else
        state = AnimatorState.Unknown;
      
      return state;
    }
  }
}
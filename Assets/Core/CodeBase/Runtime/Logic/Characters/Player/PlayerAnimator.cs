using UnityEngine;
using WC.Runtime.Infrastructure.Services;

namespace WC.Runtime.Logic.Characters
{
  public class PlayerAnimator : CharacterAnimatorBase
  {
    private readonly CharacterAnimationObserver _animObserver;
    private readonly IInputService _inputService;
    private readonly Player _player;

    public PlayerAnimator(
      Player player,
      Animator animator, 
      IInputService inputService) 
      : base(player, animator)
    {
      _inputService = inputService;
      _player = player;
    }


    public override void Tick()
    {
      if (IsActive == false) return;
      
      PlayMove();
    }


    private void PlayMove()
    {
      Animator.SetFloat(p_HorizontalHash, _player.Movement.LocalDirection.x, p_DampTime, Time.deltaTime);
      Animator.SetFloat(p_VerticalHash, _player.Movement.LocalDirection.z, p_DampTime, Time.deltaTime);

      Animator.SetBool(p_SlowWalkHash, _inputService.UnityGetSlowWalkButton());
      Animator.SetBool(p_RunHash, _inputService.UnityGetRunButton());
      Animator.SetBool(p_AimHash, _inputService.UnityGetAimButton());
    }
  }
}
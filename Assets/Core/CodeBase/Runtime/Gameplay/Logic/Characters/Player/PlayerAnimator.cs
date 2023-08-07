using UnityEngine;

namespace WC.Runtime.Gameplay.Logic
{
  public class PlayerAnimator : CharacterAnimatorBase
  {
    private readonly Player _player;

    public PlayerAnimator(
      Player player,
      Animator animator) 
      : base(player, animator)
    {
      _player = player;
    }
  }
}
using UnityEngine;

namespace WC.Runtime.Logic.Characters
{
  public class EnemyAnimator : CharacterAnimatorBase
  {
    public EnemyAnimator(Enemy enemy, Animator animator) : base(enemy, animator) {}
    
    
    public void Move(float speed)
    {
      if (IsActive == false) return;
      
      Animator.SetBool(p_IsMovingHash, true);
      Animator.SetFloat(p_SpeedHash, speed);
    }

    public void StopMove()
    {
      if (IsActive == false) return;
      
      Animator.SetBool(p_IsMovingHash, false);
      Animator.SetFloat(p_SpeedHash, 0);
    }
  }
}
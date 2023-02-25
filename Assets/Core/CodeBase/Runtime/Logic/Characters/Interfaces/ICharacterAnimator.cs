namespace WC.Runtime.Logic.Characters
{
  public interface ICharacterAnimator : IAnimator
  {
    bool IsAttacking { get; }
    
    void PlayAttack();
    void PlayHit();
    void PlayDeath();
  }
}
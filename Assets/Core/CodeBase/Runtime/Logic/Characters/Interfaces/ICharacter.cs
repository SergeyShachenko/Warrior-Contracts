namespace WC.Runtime.Logic.Characters
{
  public interface ICharacter
  {
    IAttack Attack { get; }
    IHealth Health { get; }
    IDeath Death { get; }
    ICharacterAnimator Animator { get; }
  }
}
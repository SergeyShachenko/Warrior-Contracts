using System;

namespace WC.Runtime.Logic.Characters
{
  public interface ICharacter
  {
    event Action Initialized;
    
    IAttack Attack { get; }
    IHealth Health { get; }
    IDeath Death { get; }
    IMovement Movement { get; }
    ICharacterAnimator Animator { get; }
  }
}
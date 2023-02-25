using System;

namespace WC.Runtime.Logic.Characters
{
  public interface IAttack : ILogicComponent
  {
    event Action Attack;
    float Damage { get; }
    float AttackDistance { get; }
    float Cooldown { get; }
    float HitRadius { get; }
  }
}
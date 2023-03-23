using System;

namespace WC.Runtime.Logic.Characters
{
  public interface IHealth : ILogicComponent
  {
    event Action Changed, TakingDamage;
    float Max { get; set; }
    float Current { get; set; }
    void TakeDamage(float damage);
  }
}
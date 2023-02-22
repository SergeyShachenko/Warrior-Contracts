using System;

namespace WC.Runtime.Logic.Characters
{
  public interface IHealth
  {
    event Action HealthChanged;
    float Max { get; set; }
    float Current { get; set; }
    void TakeDamage(float damage);
  }
}
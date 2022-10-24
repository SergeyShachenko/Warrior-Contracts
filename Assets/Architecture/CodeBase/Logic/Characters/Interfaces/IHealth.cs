using System;

namespace CodeBase.Logic
{
  public interface IHealth
  {
    event Action HealthChanged;
    float Max { get; }
    float Current { get; }
    void TakeDamage(float damage);
  }
}
using System;

namespace WC.Runtime.Logic.Characters
{
  public interface IDeath : ILogicComponent
  {
    event Action Happened;
    bool IsDead { get; }
    void CheckDeath(float currentHealth);
  }
}
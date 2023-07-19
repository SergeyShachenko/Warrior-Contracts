using System;

namespace WC.Runtime.Logic.Characters
{
  public abstract class CharacterDeathBase : ILogicComponent
  {
    public event Action Changed;
    public event Action Happened;
    
    public bool IsActive { get; set; } = true;
    public bool IsDead { get; protected set; }
    
    
    public virtual void Tick() { }


    public virtual void CheckDeath(float currentHealth)
    {
      if (IsActive == false) return;
      
      
      if (IsDead == false && currentHealth <= 0) 
        Die();
    }
    
    protected virtual void Die()
    {
      IsDead = true;
      Happened?.Invoke();
    }
  }
}
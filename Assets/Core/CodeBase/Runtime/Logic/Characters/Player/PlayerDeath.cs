using System;

namespace WC.Runtime.Logic.Characters
{
  public class PlayerDeath : IDeath
  {
    public event Action Happened;

    public bool IsActive { get; set; } = true;
    public bool IsDead { get; private set; }
    

    public void Tick() { }
    
    
    public void CheckDeath(float currentHealth)
    {
      if (IsActive == false) return;
      
      
      if (IsDead == false && currentHealth <= 0) 
        Die();
    }

    private void Die()
    {
      IsDead = true;
      Happened?.Invoke();
    }
  }
}
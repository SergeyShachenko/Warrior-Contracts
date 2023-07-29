using System;
using WC.Runtime.Data.Characters;

namespace WC.Runtime.Logic.Characters
{
  public abstract class CharacterHealthBase : ILogicComponent
  {
    public event Action Changed;
    public event Action TakingDamage;
    
    public bool IsActive { get; set; } = true;

    public float Current
    {
      get => p_Data.CurrentHealth;
      set
      {
        if (p_Data.CurrentHealth == value) return;

        p_Data.CurrentHealth = value;
        Changed?.Invoke();
      }
    }

    public float Max
    {
      get => p_Data.MaxHealth;
      set
      {
        if (p_Data.MaxHealth == value) return;

        p_Data.MaxHealth = value;
        Changed?.Invoke();
      }
    }
    
    protected readonly LifeStatsData p_Data;

    protected CharacterHealthBase(LifeStatsData data) => p_Data = data;


    public virtual void Tick() { }


    public virtual void TakeDamage(float damage)
    {
      if (IsActive == false) return;
      if(Current <= 0) return;
      
      
      Current -= damage;
      TakingDamage?.Invoke();
    }
  }
}
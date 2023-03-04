using System;
using WC.Runtime.Data.Characters;

namespace WC.Runtime.Logic.Characters
{
  public class PlayerHealth : IHealth
  {
    public event Action Changed, TakingDamage;

    public bool IsActive { get; set; } = true;
    public float Current
    {
      get => _progress.State.CurrentHP;
      set
      {
        if (IsActive == false) return;

        _progress.State.CurrentHP = value;
        Changed?.Invoke();
      }
    }
    public float Max
    {
      get => _progress.State.MaxHP;
      set
      {
        if (IsActive == false) return;

        _progress.State.MaxHP = value;
        Changed?.Invoke();
      }
    }
    
    private readonly PlayerProgressData _progress;

    public PlayerHealth(PlayerProgressData progress) => 
      _progress = progress;

    
    public void Tick() { }
    
    
    public void TakeDamage(float damage)
    {
      if (IsActive == false) return;
      if(Current <= 0) return;
      
      
      Current -= damage;
      TakingDamage?.Invoke();
    }
  }
}
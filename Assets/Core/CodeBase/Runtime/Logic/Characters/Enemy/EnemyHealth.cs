using System;

namespace WC.Runtime.Logic.Characters
{
  public class EnemyHealth : IHealth
  {
    public event Action Changed, TakingDamage;

    public bool IsActive { get; set; } = true;
    public float Current
    {
      get => _current;
      set
      {
        if (IsActive == false) return;
        
        _current = value;
        Changed?.Invoke();
      }
    }
    public float Max
    {
      get => _max;
      set
      {
        if (IsActive == false) return;

        _max = value;
        Changed?.Invoke();
      }
    }

    private float _max, _current;

    public EnemyHealth(float current, float max)
    {
      _current = current;
      _max = max;
    }


    public void TakeDamage(float damage)
    {
      if (IsActive == false) return;
      if (Current <= 0) return;
      
      
      Current -= damage;
      TakingDamage?.Invoke();
    }
  }
}
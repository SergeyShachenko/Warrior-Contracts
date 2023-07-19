using System;
using WC.Runtime.Data.Characters;

namespace WC.Runtime.Logic.Characters
{
  public abstract class CharacterAttackBase : ILogicComponent
  {
    public event Action Changed;
    public event Action Attack;
    
    public bool IsActive { get; set; } = true;

    public float Damage
    {
      get => p_Data.Damage;
      set
      {
        if (IsActive == false || p_Data.Damage == value) return;

        p_Data.Damage = value;
        Changed?.Invoke();
      }
    }

    public float Cooldown
    {
      get => p_Data.Cooldown;
      set
      {
        if (IsActive == false || p_Data.Cooldown == value) return;

        p_Data.Cooldown = value;
        Changed?.Invoke();
      }
    }

    public float HitRadius
    {
      get => p_Data.HitRadius;
      set
      {
        if (IsActive == false || p_Data.HitRadius == value) return;

        p_Data.HitRadius = value;
        Changed?.Invoke();
      }
    }

    public float AttackDistance
    {
      get => p_Data.AttackDistance;
      set
      {
        if (IsActive == false || p_Data.AttackDistance == value) return;

        p_Data.AttackDistance = value;
        Changed?.Invoke();
      }
    }
    
    protected readonly CombatStatsData p_Data;

    protected CharacterAttackBase(CombatStatsData data) => p_Data = data;


    public virtual void Tick() {}

    
    public abstract void TakeDamage();
    public abstract void StopAttack();

    protected virtual void StartAttack() => Attack?.Invoke();
  }
}
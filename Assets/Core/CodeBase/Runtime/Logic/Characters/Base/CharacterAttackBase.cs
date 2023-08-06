using System;
using WC.Runtime.Data.Characters;

namespace WC.Runtime.Logic.Characters
{
  public abstract class CharacterAttackBase : ILogicComponent
  {
    public event Action Changed;
    public event Action Attack, AttackEnd;
    
    public bool IsActive { get; set; } = true;

    public bool IsAiming { get; protected set; } = false;
    
    public float Damage
    {
      get => p_Data.Damage;
      set
      {
        if (p_Data.Damage == value) return;

        p_Data.Damage = value;
        Changed?.Invoke();
      }
    }

    public float Cooldown
    {
      get => p_Data.Cooldown;
      set
      {
        if (p_Data.Cooldown == value) return;

        p_Data.Cooldown = value;
        Changed?.Invoke();
      }
    }

    public float HitRadius
    {
      get => p_Data.HitRadius;
      set
      {
        if (p_Data.HitRadius == value) return;

        p_Data.HitRadius = value;
        Changed?.Invoke();
      }
    }

    public float AttackDistance
    {
      get => p_Data.AttackDistance;
      set
      {
        if (p_Data.AttackDistance == value) return;

        p_Data.AttackDistance = value;
        Changed?.Invoke();
      }
    }
    
    protected readonly CombatStatsData p_Data;
    
    private readonly CharacterBase _character;

    protected CharacterAttackBase(CharacterBase character, CombatStatsData data)
    {
      _character = character;
      p_Data = data;
    }


    public virtual void Tick() {}

    
    public abstract void DoDamage();

    public virtual void Stop() => AttackEnd?.Invoke();
    
    protected virtual void Start() => Attack?.Invoke();
  }
}
using System;
using UnityEngine.Serialization;

namespace WC.Runtime.Gameplay.Data
{
  [Serializable]
  public class CombatStatsData
  {
    public float Damage;
    public float Cooldown;
    public float HitRadius;
    public float AttackDistance;


    public CombatStatsData GetCopy() => new()
    {
      Damage = Damage,
      Cooldown = Cooldown,
      HitRadius = HitRadius,
      AttackDistance = AttackDistance
    };
  }
}
using System;
using UnityEngine;

namespace WC.Runtime.Data.Characters
{
  [Serializable]
  public class LifeStatsData
  {
    public float MaxHealth;
    public float MaxArmor;
    
    [HideInInspector] public float CurrentHealth;
    [HideInInspector] public float CurrentArmor;


    public LifeStatsData GetCopy() => new()
    {
      CurrentHealth = CurrentHealth,
      MaxHealth = MaxHealth,
      CurrentArmor = CurrentArmor,
      MaxArmor = MaxArmor
    };
  }
}
﻿using UnityEngine;
using UnityEngine.AddressableAssets;
using WC.Runtime.Logic.Characters;

namespace WC.Runtime.StaticData
{
  [CreateAssetMenu(menuName = "Dev/StaticData/Character/Enemy/Warrior", fileName = "Enemy_Warrior_TYPE_NAME")]
  public class EnemyWarriorStaticData : ScriptableObject
  {
    public WarriorID Type;
    
    [Range(1f, 100f)] public float HP = 20f;
    [Range(1f, 30f)] public float Damage = 10f;

    public int MaxLootExp;
    public int MinLootExp;
    
    [Range(0.5f, 3f)] public float AttackDistance = 2f;
    [Range(0.5f, 3f)] public float AttackCooldown = 1f;
    [Range(0.5f, 3f)] public float HitRadius = 0.5f;
    
    [Range(1f, 10f)] public float Speed = 5f;

    public AssetReferenceGameObject PrefabRef;
  }
}
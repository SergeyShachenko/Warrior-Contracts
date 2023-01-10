using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.StaticData
{
  [CreateAssetMenu(fileName = "Enemy_Warrior_TYPE_NAME", menuName = "Dev/StaticData/Character/Enemy")]
  public class WarriorStaticData : ScriptableObject
  {
    public WarriorType Type;
    
    [Range(1, 100)] public float HP = 20f;
    [Range(1, 30)] public float Damage = 10f;
    [Range(0.5f, 3)] public float AttackDistance = 2f;
    [Range(0.5f, 3)] public float AttackCooldown = 1f;
    [Range(0.5f, 3)] public float HitRadius = 0.5f;

    public GameObject Prefab;
  }
}
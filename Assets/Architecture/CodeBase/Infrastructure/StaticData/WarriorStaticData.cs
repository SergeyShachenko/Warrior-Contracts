using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.StaticData
{
  [CreateAssetMenu(fileName = "Enemy_Warrior_TYPE_NAME", menuName = "Dev/StaticData/Character/Enemy")]
  public class WarriorStaticData : ScriptableObject
  {
    public WarriorType Type;
    
    [Range(1f, 100f)] public float HP = 20f;
    [Range(1f, 30f)] public float Damage = 10f;
    [Range(0.5f, 3f)] public float AttackDistance = 2f;
    [Range(0.5f, 3f)] public float AttackCooldown = 1f;
    [Range(0.5f, 3f)] public float HitRadius = 0.5f;
    [Range(1f, 10f)] public float Speed = 5f;

    public GameObject Prefab;
  }
}
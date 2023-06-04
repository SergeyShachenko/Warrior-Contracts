using UnityEngine;
using UnityEngine.AddressableAssets;
using WC.Runtime.Logic.Characters;

namespace WC.Runtime.StaticData
{
  [CreateAssetMenu(menuName = "Dev/StaticData/Character/Player/Warrior", fileName = "Player_Warrior_TYPE_NAME")]
  public class PlayerWarriorStaticData : ScriptableObject
  {
    public WarriorID Type;
    
    [field: SerializeField, Range(1f, 100f), Header("Health")] public float HP { get; private set; } = 20f;
    
    [field: SerializeField, Range(1f, 30f), Header("Attack")] public float Damage { get; private set; } = 10f;
    [field: SerializeField, Range(0.5f, 3f)] public float AttackDistance { get; private set; } = 2f;
    [field: SerializeField, Range(0.5f, 3f)] public float AttackCooldown { get; private set; } = 1f;
    [field: SerializeField, Range(0.5f, 3f)] public float HitRadius { get; private set; } = 0.5f;
    
    [field: SerializeField, Range(1f, 10f), Header("Movement")] public float Speed { get; private set; } = 5f;

    [field: SerializeField, Header("")] public AssetReferenceGameObject PrefabRef { get; private set; }
  }
}
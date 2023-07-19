using UnityEngine;
using WC.Runtime.Data.Characters;
using WC.Runtime.Logic.Characters;

namespace WC.Runtime.StaticData
{
  [CreateAssetMenu(menuName = "Dev/StaticData/Character/Enemy/Warrior", fileName = "Enemy_Warrior_TYPE_NAME")]
  public class EnemyWarriorStaticData : CharacterStaticDataBase
  {
    [field: SerializeField] public EnemyWarriorID ID { get; private set; }
    [field: SerializeField] public EnemyStatsData Stats { get; private set; }
  }
}
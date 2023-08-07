using UnityEngine;
using WC.Runtime.Gameplay.Data;
using WC.Runtime.Gameplay.Logic;

namespace WC.Runtime.Gameplay.Data
{
  [CreateAssetMenu(menuName = "Dev/StaticData/Character/Enemy/Warrior", fileName = "Enemy_Warrior_TYPE_NAME")]
  public class EnemyWarriorStaticData : CharacterStaticDataBase
  {
    [field: SerializeField] public EnemyWarriorID ID { get; private set; }
    [field: SerializeField] public EnemyStatsData Stats { get; private set; }
  }
}
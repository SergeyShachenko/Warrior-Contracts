using UnityEngine;
using WC.Runtime.Gameplay.Logic;

namespace WC.Runtime.Gameplay.Data
{
  [CreateAssetMenu(menuName = "Dev/StaticData/Character/Enemy", fileName = "Enemy_TYPE_NAME")]
  public class EnemyStaticData : CharacterStaticDataBase
  {
    [field: SerializeField] public EnemyID ID { get; private set; }
    [field: SerializeField] public EnemyData Stats { get; private set; }
  }
}
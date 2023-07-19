using UnityEngine;
using WC.Runtime.Data.Characters;
using WC.Runtime.Logic.Characters;

namespace WC.Runtime.StaticData
{
  [CreateAssetMenu(menuName = "Dev/StaticData/Character/Player", fileName = "Player_NAME")]
  public class PlayerStaticData : CharacterStaticDataBase
  {
    [field: SerializeField] public PlayerID ID { get; private set; }
    [field: SerializeField] public PlayerStatsData Stats { get; private set; }
  }
}
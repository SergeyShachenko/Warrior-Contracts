using UnityEngine;
using WC.Runtime.Gameplay.Data;
using WC.Runtime.Gameplay.Logic;

namespace WC.Runtime.Gameplay.Data
{
  [CreateAssetMenu(menuName = "Dev/StaticData/Character/Player", fileName = "Player_NAME")]
  public class PlayerStaticData : CharacterStaticDataBase
  {
    [field: SerializeField] public PlayerID ID { get; private set; }
    [field: SerializeField] public PlayerStatsData Stats { get; private set; }
  }
}
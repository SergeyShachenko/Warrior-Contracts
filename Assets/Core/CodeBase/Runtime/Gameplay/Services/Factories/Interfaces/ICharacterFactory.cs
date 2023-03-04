using System.Threading.Tasks;
using UnityEngine;
using WC.Runtime.Infrastructure.Services;
using WC.Runtime.Logic.Characters;

namespace WC.Runtime.Gameplay.Services
{
  public interface ICharacterFactory : IFactory
  {
    Player Player { get; }
    
    Task<Player> CreatePlayer(WarriorType warriorType, Vector3 at);
    Task<GameObject> CreateEnemy(WarriorType warriorType, Transform parent);
  }
}
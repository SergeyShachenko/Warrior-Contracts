using System.Threading.Tasks;
using UnityEngine;
using WC.Runtime.Infrastructure.Services;
using WC.Runtime.Logic.Characters;

namespace WC.Runtime.Gameplay.Services
{
  public interface ICharacterFactory : IFactory<CharacterRegistry>
  {
    Task<Player> CreatePlayer(WarriorID id, Vector3 at);
    Task<Enemy> CreateEnemy(WarriorID id, Transform under);
  }
}
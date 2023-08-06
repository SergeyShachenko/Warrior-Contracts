using System.Threading.Tasks;
using UnityEngine;
using WC.Runtime.Data.Characters;
using WC.Runtime.Infrastructure.Services;
using WC.Runtime.Logic.Characters;

namespace WC.Runtime.Gameplay.Services
{
  public interface ICharacterFactory : IFactory<CharacterRegistry>
  {
    Task<Player> CreatePlayer(PlayerID id, PlayerSpawnData spawnData);
    Task<Enemy> CreateEnemy(EnemyWarriorID id, Transform under);
  }
}
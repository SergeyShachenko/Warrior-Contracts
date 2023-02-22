using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using WC.Runtime.Logic.Characters;
using WC.Runtime.Logic.Loot;
using WC.Runtime.Infrastructure.Services;

namespace WC.Runtime.Infrastructure.Services
{
  public interface IGameFactory : IService
  {
    List<ISaverProgress> ProgressSavers { get; }
    List<ILoaderProgress> ProgressLoaders { get; }
    GameObject Player { get; }

    void Register(ILoaderProgress progressLoader);
    
    Task<GameObject> CreatePlayerWarrior(WarriorType warriorType, Vector3 at);
    Task<GameObject> CreateEnemyWarrior(WarriorType warriorType, Transform parent);
    Task CreateSpawnPoint(string spawnerID, Vector3 at, WarriorType warriorType);
    Task<LootPiece> CreateLoot();
    Task<GameObject> CreateHUD();

    Task WarmUp();
    void CleanUp();
  }
}
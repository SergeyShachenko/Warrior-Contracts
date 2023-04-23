using System.Threading.Tasks;
using UnityEngine;
using WC.Runtime.Logic.Characters;
using WC.Runtime.Infrastructure.AssetManagement;
using WC.Runtime.Infrastructure.Services;

namespace WC.Runtime.Gameplay.Services
{
  public class LevelToolsFactory : FactoryBase<LevelToolsRegistry>,
    ILevelToolsFactory
  {
    public LevelToolsFactory(
      IServiceManager serviceManager,
      IAssetsProvider assetsProvider,
      ISaveLoadService saveLoadService) 
      : base(serviceManager, assetsProvider, saveLoadService)
    {
      
    }


    public async Task<EnemySpawnPoint> CreateEnemySpawnPoint(string spawnerID, Vector3 at, WarriorID warriorType)
    {
      GameObject spawnerObj = await p_AssetsProvider.InstantiateAsync(AssetAddress.SpawnPoint, at);
      RegisterProgressWatcher(spawnerObj);
      
      var spawnPoint = spawnerObj.GetComponent<EnemySpawnPoint>();
      spawnPoint.Init(warriorType, spawnerID);
      
      Registry.Register(spawnPoint);
      return spawnPoint;
    }

    public override async Task WarmUp() => 
      await p_AssetsProvider.Load<GameObject>(AssetAddress.SpawnPoint);
  }
}
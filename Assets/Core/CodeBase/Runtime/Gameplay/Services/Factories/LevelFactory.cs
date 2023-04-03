using System.Threading.Tasks;
using UnityEngine;
using WC.Runtime.Logic.Characters;
using WC.Runtime.Infrastructure.AssetManagement;
using WC.Runtime.Infrastructure.Services;

namespace WC.Runtime.Gameplay.Services
{
  public class LevelFactory : FactoryBase,
    ILevelFactory
  {
    public LevelFactory(
      IAssetsProvider assetsProvider,
      ISaveLoadRegistry saveLoadRegistry) 
      : base(assetsProvider, saveLoadRegistry)
    {
      
    }


    public async Task CreateSpawnPoint(string spawnerID, Vector3 at, WarriorID warriorType)
    {
      GameObject spawnerObj = await p_AssetsProvider.InstantiateAsync(AssetAddress.SpawnPoint, at);
      RegisterProgressWatcher(spawnerObj);
      
      var spawnPoint = spawnerObj.GetComponent<EnemySpawnPoint>();
      spawnPoint.Init(warriorType, spawnerID);
    }

    public override async Task WarmUp() => 
      await p_AssetsProvider.Load<GameObject>(AssetAddress.SpawnPoint);
  }
}
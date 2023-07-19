using System;
using System.Threading.Tasks;
using UnityEngine;
using WC.Runtime.Logic.Characters;
using WC.Runtime.Infrastructure.AssetManagement;
using WC.Runtime.Infrastructure.Services;

namespace WC.Runtime.Gameplay.Services
{
  public class LevelToolsFactory : FactoryBase<LevelToolsRegistry>,
    ILevelToolsFactory,
    IDisposable,
    IWarmUp
  {
    private readonly IAssetsProvider _assetsProvider;
    private readonly IServiceManager _serviceManager;

    public LevelToolsFactory(
      ISaveLoadService saveLoadService,
      IAssetsProvider assetsProvider,
      IServiceManager serviceManager) 
      : base(saveLoadService)
    {
      _assetsProvider = assetsProvider;
      _serviceManager = serviceManager;
      
      serviceManager.Register(this);
    }


    public async Task<EnemySpawnPoint> CreateEnemySpawnPoint(string spawnerID, Vector3 at, EnemyWarriorID warriorType)
    {
      GameObject spawnerObj = await _assetsProvider.InstantiateAsync(AssetAddress.SpawnPoint, at);

      var spawnPoint = spawnerObj.GetComponent<EnemySpawnPoint>();
      spawnPoint.Init(warriorType, spawnerID);
      
      RegisterProgressWatcher(spawnerObj);
      Registry.Register(spawnPoint);
      return spawnPoint;
    }

    async Task IWarmUp.WarmUp() => 
      await _assetsProvider.Load<GameObject>(AssetAddress.SpawnPoint);
    
    void IDisposable.Dispose() => _serviceManager.Unregister(this);
  }
}
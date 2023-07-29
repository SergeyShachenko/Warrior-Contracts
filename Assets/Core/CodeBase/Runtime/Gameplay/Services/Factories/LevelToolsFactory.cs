using System;
using System.Threading.Tasks;
using UnityEngine;
using WC.Runtime.Logic.Characters;
using WC.Runtime.Infrastructure.AssetManagement;
using WC.Runtime.Infrastructure.Services;
using WC.Runtime.Logic.Camera;

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
      GameObject spawnerObj = await _assetsProvider.InstantiateAsync(AssetAddress.Tool.EnemySpawnPoint, at);

      var spawnPoint = spawnerObj.GetComponent<EnemySpawnPoint>();
      spawnPoint.Init(warriorType, spawnerID);
      
      RegisterProgressWatcher(spawnerObj);
      Registry.Register(spawnPoint);
      return spawnPoint;
    }
    
    public async Task<PlayerCamera> CreatePlayerCamera()
    {
      GameObject cameraObj = await _assetsProvider.InstantiateAsync(AssetAddress.Tool.PlayerCamera);

      var playerCamera = cameraObj.GetComponentInChildren<PlayerCamera>();

      RegisterProgressWatcher(cameraObj);
      Registry.Register(playerCamera);
      return playerCamera;
    }

    
    async Task IWarmUp.WarmUp()
    {
      await _assetsProvider.Load<GameObject>(AssetAddress.Tool.PlayerCamera);
      await _assetsProvider.Load<GameObject>(AssetAddress.Tool.EnemySpawnPoint);
    }

    void IDisposable.Dispose() => _serviceManager.Unregister(this);
  }
}
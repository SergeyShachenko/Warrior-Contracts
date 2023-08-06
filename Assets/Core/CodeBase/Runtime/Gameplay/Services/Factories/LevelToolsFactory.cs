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
    IWarmUp,
    IDisposable 
  {
    private readonly IAssetsProvider _assetsProvider;
    private readonly IServiceManager _serviceManager;
    
    private readonly Transform _enemySpawnParent;

    public LevelToolsFactory(
      ISaveLoadService saveLoadService,
      IAssetsProvider assetsProvider,
      IServiceManager serviceManager) 
      : base(saveLoadService)
    {
      _assetsProvider = assetsProvider;
      _serviceManager = serviceManager;
      _serviceManager.Register(this);
      
      Transform spawnParent = new GameObject("SpawnPoints").transform;
      _enemySpawnParent = new GameObject("Enemies").transform;
      _enemySpawnParent.parent = spawnParent;
    }


    public async Task<EnemySpawnPoint> CreateEnemySpawnPoint(string spawnerID, EnemyWarriorID warriorType, Vector3 position, Quaternion rotation)
    {
      GameObject spawnerObj = await _assetsProvider.InstantiateAsync(AssetAddress.Tool.EnemySpawnPoint, under: _enemySpawnParent);
      spawnerObj.transform.position = position;
      spawnerObj.transform.rotation = rotation;

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
using System;
using System.Threading.Tasks;
using UnityEngine;
using WC.Runtime.Gameplay.Data;
using WC.Runtime.Infrastructure.AssetManagement;
using WC.Runtime.Infrastructure.Services;
using WC.Runtime.Gameplay.Logic;

namespace WC.Runtime.Gameplay.Services
{
  public class CharacterFactory : FactoryBase<CharacterRegistry>, 
    ICharacterFactory,
    IDisposable,
    IWarmUp
  {
    private readonly IStaticDataService _staticData;
    private readonly IAssetsProvider _assetsProvider;
    private readonly IServiceManager _serviceManager;

    public CharacterFactory(
      ISaveLoadService saveLoadService,
      IAssetsProvider assetsProvider,
      IServiceManager serviceManager,
      IStaticDataService staticData) 
      : base(saveLoadService)
    {
      _assetsProvider = assetsProvider;
      _serviceManager = serviceManager;
      _staticData = staticData;

      serviceManager.Register(this);
    }

    
    public async Task<Player> CreatePlayer(PlayerID id, PlayerSpawnData spawnData)
    {
      GameObject playerObj = await _assetsProvider.InstantiateAsync(_staticData.Players[id].PrefabRef, spawnData.Position);
      playerObj.transform.rotation = spawnData.Rotation;
      
      var player = playerObj.GetComponent<Player>();
      
      RegisterProgressWatcher(playerObj);
      Registry.Register(player);
      return player;
    }

    public async Task<Enemy> CreateEnemy(EnemyID id, Transform under)
    {
      EnemyStaticData data = _staticData.Enemies[id];
      data.Stats.Life.CurrentHealth = data.Stats.Life.MaxHealth;
      data.Stats.Life.CurrentArmor = data.Stats.Life.MaxArmor;

      GameObject enemyObj = await _assetsProvider.InstantiateAsync(data.PrefabRef, under);
      var enemy = enemyObj.GetComponent<Enemy>();
      enemy.SetData(data.ID, data.Stats.GetCopy());
      
      var lootSpawner = enemyObj.GetComponentInChildren<LootSpawner>();
      lootSpawner.SetData(data.Stats.Loot.Money);

      RegisterProgressWatcher(enemyObj);
      Registry.Register(enemy);
      return enemy;
    }

    async Task IWarmUp.WarmUp()
    {
      await _assetsProvider.Load<GameObject>(AssetAddress.Character.PlayerExoSWAT);

      foreach (EnemyStaticData warriorData in _staticData.Enemies.Values)
        await _assetsProvider.Load<GameObject>(warriorData.PrefabRef);
    }
    
    void IDisposable.Dispose() => _serviceManager.Unregister(this);
  }
}
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using WC.Runtime.Infrastructure.AssetManagement;
using WC.Runtime.Infrastructure.Services;
using WC.Runtime.Logic.Characters;
using WC.Runtime.Logic.Loot;
using WC.Runtime.StaticData;

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

    
    public async Task<Player> CreatePlayer(PlayerID id, Vector3 at)
    {
      GameObject playerObj = await _assetsProvider.InstantiateAsync(_staticData.Players[id].PrefabRef, at);
      var player = playerObj.GetComponent<Player>();
      
      RegisterProgressWatcher(playerObj);
      Registry.Register(player);
      return player;
    }

    public async Task<Enemy> CreateEnemy(EnemyWarriorID id, Transform under)
    {
      EnemyWarriorStaticData staticData = _staticData.EnemyWarriors[id];
      staticData.Stats.Life.CurrentHealth = staticData.Stats.Life.MaxHealth;
      staticData.Stats.Life.CurrentArmor = staticData.Stats.Life.MaxArmor;

      GameObject enemyObj = await _assetsProvider.InstantiateAsync(staticData.PrefabRef, under);
      var enemy = enemyObj.GetComponent<Enemy>();

      enemy.SetData(Registry.Player, staticData);

      if (enemyObj.TryGetComponent(out RotateToPlayerAI rotateToPlayer))
      {
        rotateToPlayer.Speed = staticData.Stats.Movement.RunSpeed;
      }

      enemyObj.GetComponent<NavMeshAgent>().speed = staticData.Stats.Movement.RunSpeed;
      
      var lootSpawner = enemyObj.GetComponentInChildren<LootSpawner>();
      lootSpawner.SetData(staticData.Stats.Loot.Money);

      RegisterProgressWatcher(enemyObj);
      Registry.Register(enemy);
      return enemy;
    }

    async Task IWarmUp.WarmUp()
    {
      await _assetsProvider.Load<GameObject>(AssetAddress.Character.PlayerExoSWAT);

      foreach (EnemyWarriorStaticData warriorData in _staticData.EnemyWarriors.Values)
        await _assetsProvider.Load<GameObject>(warriorData.PrefabRef);
    }
    
    void IDisposable.Dispose() => _serviceManager.Unregister(this);
  }
}
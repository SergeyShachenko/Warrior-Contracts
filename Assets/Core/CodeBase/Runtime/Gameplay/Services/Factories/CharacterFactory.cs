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

    
    public async Task<Player> CreatePlayer(WarriorID id, Vector3 at)
    {
      GameObject playerObj = await _assetsProvider.InstantiateAsync(AssetAddress.Character.PlayerSword, at);
      var player = playerObj.GetComponent<Player>();
      
      RegisterProgressWatcher(playerObj);
      Registry.Register(player);
      return Registry.Player;
    }

    public async Task<Enemy> CreateEnemy(WarriorID id, Transform under)
    {
      EnemyWarriorStaticData warriorData = _staticData.EnemyWarriors[id];
      GameObject warriorObj = await _assetsProvider.InstantiateAsync(warriorData.PrefabRef, under);
      var enemy = warriorObj.GetComponent<Enemy>();

      enemy.Construct(
        player: Registry.Player,
        id: id,
        currentHP: warriorData.HP, 
        maxHP: warriorData.HP, 
        damage: warriorData.Damage, 
        attackDistance: warriorData.AttackDistance, 
        hitRadius: warriorData.HitRadius, 
        cooldown: warriorData.AttackCooldown);

      if (warriorObj.TryGetComponent(out RotateToPlayerAI rotateToPlayer))
      {
        rotateToPlayer.Speed = warriorData.Speed;
      }

      warriorObj
        .GetComponent<NavMeshAgent>()
        .speed = warriorData.Speed;
      
      var lootSpawner = warriorObj.GetComponentInChildren<LootSpawner>();
      lootSpawner.Init(warriorData.MinLootExp, warriorData.MaxLootExp);

      RegisterProgressWatcher(warriorObj);
      Registry.Register(enemy);
      return enemy;
    }

    async Task IWarmUp.WarmUp()
    {
      await _assetsProvider.Load<GameObject>(AssetAddress.Character.PlayerSword);

      foreach (EnemyWarriorStaticData warriorData in _staticData.EnemyWarriors.Values)
        await _assetsProvider.Load<GameObject>(warriorData.PrefabRef);
    }
    
    void IDisposable.Dispose() => _serviceManager.Unregister(this);
  }
}
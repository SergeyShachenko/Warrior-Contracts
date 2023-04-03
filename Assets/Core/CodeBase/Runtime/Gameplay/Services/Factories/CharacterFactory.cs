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
  public class CharacterFactory : FactoryBase, 
    ICharacterFactory
  {
    private readonly ICharacterRegistry _registry;
    private readonly IStaticDataService _staticData;

    public CharacterFactory(
      IAssetsProvider assetsProvider,
      ISaveLoadRegistry saveLoadRegistry,
      ICharacterRegistry registry,
      IStaticDataService staticData) 
      : base(assetsProvider, saveLoadRegistry)
    {
      _registry = registry;
      _staticData = staticData;
    }

    
    public async Task<Player> CreatePlayer(WarriorID id, Vector3 at)
    {
      GameObject playerObj = await p_AssetsProvider.InstantiateAsync(AssetAddress.Character.PlayerSword, at);
      RegisterProgressWatcher(playerObj);
      
      _registry.Register(playerObj.GetComponent<Player>());
      return _registry.Player;
    }

    public async Task<Enemy> CreateEnemy(WarriorID id, Transform under)
    {
      EnemyWarriorStaticData warriorData = _staticData.GetEnemyWarrior(id);
      GameObject warriorObj = await p_AssetsProvider.InstantiateAsync(warriorData.PrefabRef, under);
      RegisterProgressWatcher(warriorObj);
      
      var enemy = warriorObj.GetComponent<Enemy>();

      enemy.Construct(
        player: _registry.Player,
        id: id,
        currentHP: warriorData.HP, 
        maxHP: warriorData.HP, 
        damage: warriorData.Damage, 
        attackDistance: warriorData.AttackDistance, 
        hitRadius: warriorData.HitRadius, 
        cooldown: warriorData.AttackCooldown);

      if (warriorObj.TryGetComponent(out RotateToPlayerAI rotateToPlayer))
      {
        rotateToPlayer.Init(_registry.Player.gameObject);
        rotateToPlayer.Speed = warriorData.Speed;
      }
      
      if (warriorObj.TryGetComponent(out MoveToPlayerAI moveToPlayerAI)) 
        moveToPlayerAI.Init(_registry.Player.gameObject);
      
      warriorObj
        .GetComponent<NavMeshAgent>()
        .speed = warriorData.Speed;
      
      var lootSpawner = warriorObj.GetComponentInChildren<LootSpawner>();
      lootSpawner.Init(warriorData.MinLootExp, warriorData.MaxLootExp);

      _registry.Register(enemy);
      return enemy;
    }

    public override async Task WarmUp()
    {
      await p_AssetsProvider.Load<GameObject>(AssetAddress.Character.PlayerSword);
      //await p_AssetsProvider.Load<GameObject>(warriorData.PrefabRef);
    }
  }
}
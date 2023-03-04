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
    private readonly IStaticDataService _staticData;
    private readonly ILootFactory _lootFactory;
    private readonly IInputService _inputService;
    private readonly IRandomService _randomService;

    public CharacterFactory(
      IAssetsProvider assetsProvider,
      IStaticDataService staticData,
      ILootFactory lootFactory,
      IInputService inputService,
      IRandomService randomService) 
      : base(assetsProvider)
    {
      _staticData = staticData;
      _lootFactory = lootFactory;
      _inputService = inputService;
      _randomService = randomService;
    }

    public Player Player { get; private set; }
    
    public async Task<Player> CreatePlayer(WarriorType warriorType, Vector3 at)
    {
      GameObject player = await InstantiateAsync(AssetAddress.Character.PlayerSword, at);
      Player = player.GetComponent<Player>();
      Player.Construct(_inputService);
      
      return Player;
    }

    public async Task<GameObject> CreateEnemy(WarriorType warriorType, Transform parent)
    {
      EnemyWarriorStaticData warriorData = _staticData.GetEnemyWarrior(warriorType);
      var warriorPref = await p_AssetsProvider.Load<GameObject>(warriorData.PrefabRef);
      
      
      GameObject warrior = Object.Instantiate(warriorPref, parent.position, parent.rotation, parent);
      var enemy = warrior.GetComponent<Enemy>();
      
      enemy.Construct(
        player: Player,
        currentHP: warriorData.HP, 
        maxHP: warriorData.HP, 
        damage: warriorData.Damage, 
        attackDistance: warriorData.AttackDistance, 
        hitRadius: warriorData.HitRadius, 
        cooldown: warriorData.AttackCooldown);

      if (warrior.TryGetComponent(out RotateToPlayerAI rotateToPlayer))
      {
        rotateToPlayer.Construct(Player.gameObject);
        rotateToPlayer.Speed = warriorData.Speed;
      }
      
      if (warrior.TryGetComponent(out MoveToPlayerAI moveToPlayerAI)) 
        moveToPlayerAI.Construct(Player.gameObject);
      
      warrior.GetComponent<NavMeshAgent>().speed = warriorData.Speed;
      
      var lootSpawner = warrior.GetComponentInChildren<LootSpawner>();
      lootSpawner.Construct(_lootFactory, _randomService);
      lootSpawner.SetLootExp(warriorData.MinLootExp, warriorData.MaxLootExp);

      return warrior;
    }

    public override Task WarmUp() => 
      Task.CompletedTask;
  }
}
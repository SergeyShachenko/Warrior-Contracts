using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using WC.Runtime.UI.Services;
using WC.Runtime.UI.Elements;
using WC.Runtime.Logic.Characters;
using WC.Runtime.Logic.Loot;
using WC.Runtime.Infrastructure.AssetManagement;
using WC.Runtime.StaticData;
using WC.Runtime.UI.HUD;
using Object = UnityEngine.Object;

namespace WC.Runtime.Infrastructure.Services
{
  public class GameFactory : IGameFactory
  {
    public List<ISaverProgress> ProgressSavers { get; } = new();
    public List<ILoaderProgress> ProgressLoaders { get; } = new();
    public GameObject Player { get; private set; }

    private readonly IPersistentProgressService _progressService;
    private readonly IAssetsProvider _assetsProvider;
    private readonly IStaticDataService _staticData;
    private readonly IRandomService _randomService;
    private readonly IWindowService _windowService;
    private readonly IInputService _inputService;

    public GameFactory(
      IPersistentProgressService progressService,
      IAssetsProvider assetsProvider,
      IStaticDataService staticData,
      IRandomService randomService,
      IWindowService windowService, 
      IInputService inputService)
    {
      _progressService = progressService;
      _assetsProvider = assetsProvider;
      _staticData = staticData;
      _randomService = randomService;
      _windowService = windowService;
      _inputService = inputService;
    }


    public void Register(ILoaderProgress progressLoader)
    {
      if(progressLoader is ISaverProgress progressSaver)
        ProgressSavers.Add(progressSaver);
      
      ProgressLoaders.Add(progressLoader);
    }

    public async Task<GameObject> CreatePlayerWarrior(WarriorType warriorType, Vector3 at)
    {
      Player = await InstantiateRegisteredAsync(AssetAddress.PlayerSword, at);
      Player.GetComponent<Player>().Construct(_inputService);
      return Player;
    }

    public async Task<GameObject> CreateEnemyWarrior(WarriorType warriorType, Transform parent)
    {
      EnemyWarriorStaticData warriorData = _staticData.ForEnemyWarrior(warriorType);
      var warriorPref = await _assetsProvider.Load<GameObject>(warriorData.PrefabRef);
      
      
      GameObject warrior = Object.Instantiate(warriorPref, parent.position, parent.rotation, parent);
      var enemy = warrior.GetComponent<Enemy>();
      
      enemy.Construct(
        player: Player.GetComponent<Player>(),
        currentHP: warriorData.HP, 
        maxHP: warriorData.HP, 
        damage: warriorData.Damage, 
        attackDistance: warriorData.AttackDistance, 
        hitRadius: warriorData.HitRadius, 
        cooldown: warriorData.AttackCooldown);

      if (warrior.TryGetComponent(out RotateToPlayerAI rotateToPlayer))
      {
        rotateToPlayer.Construct(Player);
        rotateToPlayer.Speed = warriorData.Speed;
      }
      
      if (warrior.TryGetComponent(out MoveToPlayerAI moveToPlayerAI)) 
        moveToPlayerAI.Construct(Player);

      warrior.GetComponentInChildren<ActorHUD>().Construct(enemy.Health);
      warrior.GetComponent<NavMeshAgent>().speed = warriorData.Speed;
      
      var lootSpawner = warrior.GetComponentInChildren<LootSpawner>();
      lootSpawner.Construct(this, _randomService);
      lootSpawner.SetLootExp(warriorData.MinLootExp, warriorData.MaxLootExp);

      return warrior;
    }

    public async Task CreateSpawnPoint(string spawnerID, Vector3 at, WarriorType warriorType)
    {
      var spawnerObj = await _assetsProvider.Load<GameObject>(AssetAddress.SpawnPoint);
      
      
      var spawnPoint = InstantiateRegisteredAsync(spawnerObj, at)
        .GetComponent<EnemySpawnPoint>();
      
      spawnPoint.Construct(this);
      spawnPoint.ID = spawnerID;
      spawnPoint.WarriorType = warriorType;
    }

    public async Task<LootPiece> CreateLoot()
    {
      var lootObj = await _assetsProvider.Load<GameObject>(AssetAddress.Loot);
      
      
      var lootPiece = InstantiateRegisteredAsync(lootObj).GetComponent<LootPiece>();
      lootPiece.Construct(_progressService.Progress.World);
      
      return lootPiece; 
    }

    public async Task<GameObject> CreateHUD()
    {
      GameObject hud = await InstantiateRegisteredAsync(AssetAddress.HUD);
      hud.GetComponentInChildren<LootCounter>()
        .Construct(_progressService.Progress.World);

      foreach (OpenWindowButton button in hud.GetComponentsInChildren<OpenWindowButton>())
        button.Construct(_windowService);

      return hud;
    }

    public async Task WarmUp()
    {
      await _assetsProvider.Load<GameObject>(AssetAddress.Loot);
      await _assetsProvider.Load<GameObject>(AssetAddress.SpawnPoint);
    }

    public void CleanUp()
    {
      ProgressSavers.Clear();
      ProgressLoaders.Clear();
      _assetsProvider.CleanUp();
    }

    private GameObject InstantiateRegisteredAsync(GameObject prefab)
    {
      GameObject gameObject = Object.Instantiate(prefab);
      RegisterProgressLoaders(gameObject);
      return gameObject;
    }
    
    private async Task<GameObject> InstantiateRegisteredAsync(string address)
    {
      GameObject gameObject = await _assetsProvider.Instantiate(address);
      RegisterProgressLoaders(gameObject);
      return gameObject;
    }

    private async Task<GameObject> InstantiateRegisteredAsync(string address, Vector3 at)
    {
      GameObject gameObject = await _assetsProvider.Instantiate(address, at);
      RegisterProgressLoaders(gameObject);
      return gameObject;
    }
    
    private GameObject InstantiateRegisteredAsync(GameObject prefab, Vector3 at)
    {
      GameObject gameObject = Object.Instantiate(prefab, position: at, Quaternion.identity);
      RegisterProgressLoaders(gameObject);
      return gameObject;
    }

    private void RegisterProgressLoaders(GameObject gameObject)
    {
      foreach (ILoaderProgress progressLoader in gameObject.GetComponentsInChildren<ILoaderProgress>())
        RegisterProgressLoader(progressLoader);
    }

    private void RegisterProgressLoader(ILoaderProgress progressLoader)
    {
      if (progressLoader is ISaverProgress progressSaver) 
        ProgressSavers.Add(progressSaver);

      ProgressLoaders.Add(progressLoader);
    }
  }
}
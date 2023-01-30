using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.StaticData;
using CodeBase.Logic;
using CodeBase.Logic.Characters;
using CodeBase.Logic.Loot;
using CodeBase.StaticData;
using CodeBase.UI.HUD;
using CodeBase.UI.HUD.Character;
using CodeBase.UI.Services;
using CodeBase.UI.UIElements;
using UnityEngine;
using UnityEngine.AI;
using Object = UnityEngine.Object;

namespace CodeBase.Infrastructure.Factories
{
  public class GameFactory : IGameFactory
  {
    public List<ISaverProgress> ProgressSavers { get; } = new();
    public List<ILoaderProgress> ProgressLoaders { get; } = new();
    public GameObject Player { get; private set; }

    private readonly IPersistentProgressService _progressService;
    private readonly IAssets _assets;
    private readonly IStaticDataService _staticData;
    private readonly IRandomService _randomService;
    private readonly IWindowService _windowService;

    public GameFactory(
      IPersistentProgressService progressService, 
      IAssets assets,
      IStaticDataService staticData, 
      IRandomService randomService,
      IWindowService windowService)
    {
      _progressService = progressService;
      _assets = assets;
      _staticData = staticData;
      _randomService = randomService;
      _windowService = windowService;
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
      return Player;
    }

    public async Task<GameObject> CreateEnemyWarrior(WarriorType warriorType, Transform parent)
    {
      EnemyWarriorStaticData enemyWarriorData = _staticData.ForEnemyWarrior(warriorType);
      var warriorPref = await _assets.Load<GameObject>(enemyWarriorData.PrefabRef);
      
      
      GameObject warrior = Object.Instantiate(warriorPref, parent.position, parent.rotation, parent);

      var health = warrior.GetComponent<IHealth>();
      health.Current = enemyWarriorData.HP;
      health.Max = enemyWarriorData.HP;

      var attack = warrior.GetComponentInChildren<EnemyAttack>();
      attack.Construct(Player);
      attack.Damage = enemyWarriorData.Damage;
      attack.AttackDistance = enemyWarriorData.AttackDistance;
      attack.AttackCooldown = enemyWarriorData.AttackCooldown;
      attack.HitRadius = enemyWarriorData.HitRadius;

      if (warrior.TryGetComponent(out RotateToPlayerAI rotateToPlayer))
      {
        rotateToPlayer.Construct(Player);
        rotateToPlayer.Speed = enemyWarriorData.Speed;
      }
      
      if (warrior.TryGetComponent(out MoveToPlayerAI moveToPlayerAI)) 
        moveToPlayerAI.Construct(Player);

      warrior.GetComponentInChildren<ActorHUD>().Construct(health);
      warrior.GetComponent<NavMeshAgent>().speed = enemyWarriorData.Speed;
      
      var lootSpawner = warrior.GetComponentInChildren<LootSpawner>();
      lootSpawner.Construct(this, _randomService);
      lootSpawner.SetLootExp(enemyWarriorData.MinLootExp, enemyWarriorData.MaxLootExp);

      return warrior;
    }

    public async Task CreateSpawnPoint(string spawnerID, Vector3 at, WarriorType warriorType)
    {
      var spawnerObj = await _assets.Load<GameObject>(AssetAddress.SpawnPoint);
      
      
      var spawnPoint = InstantiateRegisteredAsync(spawnerObj, at)
        .GetComponent<SpawnPoint>();
      
      spawnPoint.Construct(this);
      spawnPoint.ID = spawnerID;
      spawnPoint.WarriorType = warriorType;
    }

    public async Task<LootPiece> CreateLoot()
    {
      var lootObj = await _assets.Load<GameObject>(AssetAddress.Loot);
      
      
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
      await _assets.Load<GameObject>(AssetAddress.Loot);
      await _assets.Load<GameObject>(AssetAddress.SpawnPoint);
    }

    public void CleanUp()
    {
      ProgressSavers.Clear();
      ProgressLoaders.Clear();
      _assets.CleanUp();
    }

    private GameObject InstantiateRegisteredAsync(GameObject prefab)
    {
      GameObject gameObject = Object.Instantiate(prefab);
      RegisterProgressWatchers(gameObject);
      return gameObject;
    }
    
    private async Task<GameObject> InstantiateRegisteredAsync(string address)
    {
      GameObject gameObject = await _assets.Instantiate(address);
      RegisterProgressWatchers(gameObject);
      return gameObject;
    }

    private async Task<GameObject> InstantiateRegisteredAsync(string address, Vector3 at)
    {
      GameObject gameObject = await _assets.Instantiate(address, at);
      RegisterProgressWatchers(gameObject);
      return gameObject;
    }
    
    private GameObject InstantiateRegisteredAsync(GameObject prefab, Vector3 at)
    {
      GameObject gameObject = Object.Instantiate(prefab, position: at, Quaternion.identity);
      RegisterProgressWatchers(gameObject);
      return gameObject;
    }

    private void RegisterProgressWatchers(GameObject gameObject)
    {
      foreach (ILoaderProgress progressLoader in gameObject.GetComponentsInChildren<ILoaderProgress>())
        RegisterProgressWatcher(progressLoader);
    }

    private void RegisterProgressWatcher(ILoaderProgress progressLoader)
    {
      if (progressLoader is ISaverProgress progressSaver) 
        ProgressSavers.Add(progressSaver);

      ProgressLoaders.Add(progressLoader);
    }
  }
}
using System.Collections.Generic;
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
    private readonly IAssetsProvider _assetsProvider;
    private readonly IStaticDataService _staticData;
    private readonly IRandomService _randomService;
    private readonly IWindowService _windowService;

    public GameFactory(
      IPersistentProgressService progressService, 
      IAssetsProvider assetsProvider,
      IStaticDataService staticData, 
      IRandomService randomService,
      IWindowService windowService)
    {
      _progressService = progressService;
      _assetsProvider = assetsProvider;
      _staticData = staticData;
      _randomService = randomService;
      _windowService = windowService;
    }
    
    
    public GameObject CreatePlayer(Vector3 at)
    {
      Player = InstantiateRegistered(AssetPath.PlayerSword, at);
      return Player;
    }

    public GameObject CreateEnemyWarrior(WarriorType warriorType, Transform parent)
    {
      WarriorStaticData warriorData = _staticData.ForWarrior(warriorType);
      GameObject warrior = Object.Instantiate(warriorData.Prefab, parent.position, parent.rotation, parent);

      var health = warrior.GetComponent<IHealth>();
      health.Current = warriorData.HP;
      health.Max = warriorData.HP;

      var attack = warrior.GetComponentInChildren<EnemyAttack>();
      attack.Construct(Player);
      attack.Damage = warriorData.Damage;
      attack.AttackDistance = warriorData.AttackDistance;
      attack.AttackCooldown = warriorData.AttackCooldown;
      attack.HitRadius = warriorData.HitRadius;

      if (warrior.TryGetComponent(out RotateToPlayerAI rotateToPlayer))
      {
        rotateToPlayer.Construct(Player);
        rotateToPlayer.Speed = warriorData.Speed;
      }
      
      if (warrior.TryGetComponent(out MoveToPlayerAI moveToPlayerAI)) 
        moveToPlayerAI.Construct(Player);

      warrior.GetComponentInChildren<ActorHUD>().Construct(health);
      warrior.GetComponent<NavMeshAgent>().speed = warriorData.Speed;
      
      var lootSpawner = warrior.GetComponentInChildren<LootSpawner>();
      lootSpawner.Construct(this, _randomService);
      lootSpawner.SetLootExp(warriorData.MinLootExp, warriorData.MaxLootExp);

      return warrior;
    }

    public void CreateSpawnPoint(string spawnerID, Vector3 at, WarriorType warriorType)
    {
      var spawner = InstantiateRegistered(AssetPath.SpawnPoint, at)
        .GetComponent<SpawnPoint>();
      spawner.Construct(this);
      spawner.ID = spawnerID;
      spawner.WarriorType = warriorType;
    }

    public LootPiece CreateLoot()
    {
      var lootPiece = InstantiateRegistered(AssetPath.LootExp).GetComponent<LootPiece>();
      lootPiece.Construct(_progressService.Progress.World);
      return lootPiece; 
    }

    public GameObject CreateHUD()
    {
      GameObject hud = InstantiateRegistered(AssetPath.HUD);
      hud.GetComponentInChildren<LootCounter>()
        .Construct(_progressService.Progress.World);

      foreach (OpenWindowButton button in hud.GetComponentsInChildren<OpenWindowButton>())
      {
        button.Construct(_windowService);
      }
      
      return hud;
    }

    public void CleanUp()
    {
      ProgressSavers.Clear();
      ProgressLoaders.Clear();
    }
    
    private void Register(ILoaderProgress progressLoader)
    {
      if(progressLoader is ISaverProgress progressSaver)
        ProgressSavers.Add(progressSaver);
      
      ProgressLoaders.Add(progressLoader);
    }

    private GameObject InstantiateRegistered(string prefabPath, Vector3 at)
    {
      GameObject gameObject = _assetsProvider.Instantiate(path: prefabPath, position: at);
      RegisterProgressWatchers(gameObject);
      return gameObject;
    }
    
    private GameObject InstantiateRegistered(string prefabPath)
    {
      GameObject gameObject = _assetsProvider.Instantiate(path: prefabPath);
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
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

    public GameFactory(
      IPersistentProgressService progressService, 
      IAssets assets,
      IStaticDataService staticData, 
      IRandomService randomService)
    {
      _progressService = progressService;
      _assets = assets;
      _staticData = staticData;
      _randomService = randomService;
    }
    
    
    public GameObject CreatePlayer(GameObject at)
    {
      Player = InstantiateRegistered(AssetPath.PlayerSword, at.transform.position);
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
      
      return hud;
    }

    public void CleanUp()
    {
      ProgressSavers.Clear();
      ProgressLoaders.Clear();
    }
    
    public void Register(ILoaderProgress progressLoader)
    {
      if(progressLoader is ISaverProgress progressSaver)
        ProgressSavers.Add(progressSaver);
      
      ProgressLoaders.Add(progressLoader);
    }

    private GameObject InstantiateRegistered(string prefabPath, Vector3 at)
    {
      GameObject gameObject = _assets.Instantiate(path: prefabPath, position: at);
      RegisterProgressWatchers(gameObject);
      return gameObject;
    }
    
    private GameObject InstantiateRegistered(string prefabPath)
    {
      GameObject gameObject = _assets.Instantiate(path: prefabPath);
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
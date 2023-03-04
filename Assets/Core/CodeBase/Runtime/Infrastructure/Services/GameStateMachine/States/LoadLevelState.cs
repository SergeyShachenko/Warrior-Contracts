using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using WC.Runtime.UI.Services;
using WC.Runtime.UI.Screens;
using WC.Runtime.Logic.Camera;
using WC.Runtime.Logic.Characters;
using WC.Runtime.Data.Characters;
using WC.Runtime.Gameplay.Services;
using WC.Runtime.StaticData;
using WC.Runtime.UI;
using Zenject;

namespace WC.Runtime.Infrastructure.Services
{
  public class LoadLevelState : IPayloadState<string>
  {
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly ILoadingScreen _loadingScreen;
    private readonly IPersistentProgressService _progressService;
    private readonly IStaticDataService _staticDataService;
    private readonly IUIRegistry _uiRegistry;
    private readonly IUIFactory _uiFactory;
    private readonly IHUDFactory _hudFactory;
    private readonly ILootFactory _lootFactory;
    private readonly ICharacterFactory _characterFactory;
    private readonly ILevelFactory _levelFactory;

    private Player _player;
    private GameObject _ui, _hud;

    private Action _onExit;

    public LoadLevelState(GameStateMachine stateMachine, DiContainer container)
    {
      _stateMachine = stateMachine;
      _sceneLoader = container.Resolve<SceneLoader>();
      _loadingScreen = container.Resolve<ILoadingScreen>();
      
      _progressService = container.Resolve<IPersistentProgressService>();
      _staticDataService = container.Resolve<IStaticDataService>();
      
      _uiRegistry = container.Resolve<IUIRegistry>();
      _uiFactory = container.Resolve<IUIFactory>();
      _hudFactory = container.Resolve<IHUDFactory>();
      
      _lootFactory = container.Resolve<ILootFactory>();
      _characterFactory = container.Resolve<ICharacterFactory>();
      _levelFactory = container.Resolve<ILevelFactory>();
    }


    public void Enter(string sceneName, Action onExit = null)
    {
      _onExit = onExit;      
      
      _loadingScreen.Show();
      
      CleanUpServices();
      WarmUpServices();

      _sceneLoader.HotLoad(sceneName, OnLoaded);
    }

    public void Exit()
    {
      _loadingScreen.Hide();
      _onExit?.Invoke();
    }

    private async void OnLoaded()
    {
      await CreateGameWorld();
      await CreateUI();
      await CreateHUD();

      InformProgressLoaders();
      InitCamera(_player.gameObject);

      _stateMachine.Enter<GameLoopState>();
    }

    private async Task CreateUI()
    {
      await _uiFactory.CreateUI();
      await _uiFactory.Create(UIWindowID.Shop);
    }

    private async Task CreateHUD()
    {
      await _hudFactory.CreateHUD(_player);
    }

    private async Task CreateGameWorld()
    {
      LevelStaticData levelData = GetLevelStaticData();
      await CreateSpawners(levelData);
 
      // TODO Сделать механику сохранения лута
      //await InitDroppedLoot();
      
      _player = await CreatePlayer(levelData);
    }

    private async Task CreateSpawners(LevelStaticData levelData)
    {
      foreach (EnemySpawnerData spawner in levelData.EnemySpawners)
        await _levelFactory.CreateSpawnPoint(spawner.ID, spawner.Position, spawner.WarriorType);
    }

    private async Task<Player> CreatePlayer(LevelStaticData levelData) => 
      await _characterFactory.CreatePlayer(WarriorType.Sword, levelData.InitPlayerPos);

    // private async Task InitDroppedLoot()
    // {
    //   DroppedLoot droppedLoot = _progressService.Progress.World.DroppedLoot;
    //
    //   foreach (DroppedItem drop in droppedLoot.Items)
    //   {
    //     LootPiece loot = await _gameFactory.CreateLoot();
    //     loot.transform.position = drop.Position.AsUnityVector(); // AsUnityVector() - экстеншн
    //     loot.Init(drop.Loot);
    //   }
    // }

    private void WarmUpServices()
    {
      _uiFactory.WarmUp();
      _hudFactory.WarmUp();
      _lootFactory.WarmUp();
      _characterFactory.WarmUp();
      _levelFactory.WarmUp();
    }

    private void CleanUpServices()
    {
      _uiRegistry.CleanUp();
      _uiFactory.CleanUp();
      _hudFactory.CleanUp();
      _lootFactory.CleanUp();
      _characterFactory.CleanUp();
      _levelFactory.CleanUp();
    }

    private LevelStaticData GetLevelStaticData() => 
      _staticDataService.GetLevel(SceneManager.GetActiveScene().name);

    private void InformProgressLoaders()
    {
      PlayerProgressData progress = _progressService.Player.Copy();
      
      foreach (ILoaderProgress progressLoader in _uiFactory.ProgressLoaders)
        progressLoader.LoadProgress(progress);
      
      foreach (ILoaderProgress progressLoader in _hudFactory.ProgressLoaders)
        progressLoader.LoadProgress(progress);
      
      foreach (ILoaderProgress progressLoader in _lootFactory.ProgressLoaders)
        progressLoader.LoadProgress(progress);
      
      foreach (ILoaderProgress progressLoader in _characterFactory.ProgressLoaders)
        progressLoader.LoadProgress(progress);
      
      foreach (ILoaderProgress progressLoader in _levelFactory.ProgressLoaders)
        progressLoader.LoadProgress(progress);
    }

    private void InitCamera(GameObject target) =>
      Camera.main.GetComponent<CameraMover>().Follow(target);
  }
}
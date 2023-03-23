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
  public class LoadLevelState : PayloadGameStateBase<DiContainer>
  {
    private readonly IPersistentProgressService _progressService;
    private readonly ILoadingScreen _loadingScreen;
    private readonly IStaticDataService _staticData;
    private readonly IUIFactory _uiFactory;

    private IHUDFactory _hudFactory;
    private ILootFactory _lootFactory;
    private ICharacterFactory _characterFactory;
    private ILevelFactory _levelFactory;

    private Player _player;

    public LoadLevelState(GameStateMachine stateMachine, DiContainer container) : base(stateMachine, container)
    {
      _loadingScreen = Container.Resolve<ILoadingScreen>();
      _progressService = Container.Resolve<IPersistentProgressService>();
      _staticData = Container.Resolve<IStaticDataService>();
      _uiFactory = Container.Resolve<IUIFactory>();
    }


    public override async void Enter(DiContainer subContainer, Action onExit = null)
    {
      BindSubServices(subContainer);
      
      WarmUpServices();
      
      await CreateGameWorld();
      await CreateUI();
      await CreateHUD();

      LoadProgress();
      InitCamera(_player.gameObject);

      base.Enter(subContainer, onExit);
      
      StateMachine.Enter<GameLoopState, DiContainer>(subContainer);
    }

    public override void Exit()
    {
      _loadingScreen.Hide();
      
      base.Exit();
    }

    private void BindSubServices(DiContainer subContainer)
    {
      _hudFactory = subContainer.Resolve<IHUDFactory>();
      _lootFactory = subContainer.Resolve<ILootFactory>();
      _characterFactory = subContainer.Resolve<ICharacterFactory>();
      _levelFactory = subContainer.Resolve<ILevelFactory>();
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
      LevelStaticData levelData = _staticData.GetLevel(SceneManager.GetActiveScene().name);;
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

    private void LoadProgress()
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
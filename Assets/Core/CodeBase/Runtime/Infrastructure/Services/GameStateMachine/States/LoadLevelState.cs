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
    private readonly ISaveLoadService _saveLoadService;
    private readonly ILoadingScreen _loadingScreen;
    private readonly IStaticDataService _staticData;

    private ICharacterRegistry _characterRegistry;
    
    private ICharacterFactory _characterFactory;
    private ILevelFactory _levelFactory;
    private ILootFactory _lootFactory;

    private IUIFactory _uiFactory;
    private IHUDFactory _hudFactory;

    public LoadLevelState(GameStateMachine stateMachine, DiContainer container) : base(stateMachine, container)
    {
      _loadingScreen = container.Resolve<ILoadingScreen>();
      _saveLoadService = container.Resolve<ISaveLoadService>();
      _staticData = container.Resolve<IStaticDataService>();
    }


    public override async void Enter(DiContainer subContainer, Action onExit = null)
    {
      BindSubServices(subContainer);
      WarmUp();
      
      await CreateGameWorld();
      await CreateUI();
      await CreateHUD();
      InitCamera();
      
      _saveLoadService.LoadProgress();

      base.Enter(subContainer, onExit);
      
      p_StateMachine.Enter<GameLoopState, DiContainer>(subContainer);
    }

    public override void Exit()
    {
      _loadingScreen.Hide();
      
      base.Exit();
    }

    
    private void BindSubServices(DiContainer subContainer)
    {
      _characterRegistry = subContainer.Resolve<ICharacterRegistry>();
      
      _characterFactory = subContainer.Resolve<ICharacterFactory>();
      _levelFactory = subContainer.Resolve<ILevelFactory>();
      _lootFactory = subContainer.Resolve<ILootFactory>();
      
      _uiFactory = subContainer.Resolve<IUIFactory>();
      _hudFactory = subContainer.Resolve<IHUDFactory>();
    }

    private async Task CreateUI()
    {
      await _uiFactory.CreateUI();
      await _uiFactory.Create(UIWindowID.Shop);
    }

    private async Task CreateHUD()
    {
      await _hudFactory.CreateHUD();
    }

    private async Task CreateGameWorld()
    {
      LevelStaticData levelData = _staticData.GetLevel(SceneManager.GetActiveScene().name);;
      await CreateSpawners(levelData);
      await CreatePlayer(levelData);

      // TODO Сделать механику сохранения лута
      //await InitDroppedLoot();
    }

    private async Task CreateSpawners(LevelStaticData levelData)
    {
      foreach (EnemySpawnerData spawner in levelData.EnemySpawners)
        await _levelFactory.CreateSpawnPoint(spawner.ID, spawner.Position, spawner.WarriorType);
    }

    private async Task CreatePlayer(LevelStaticData levelData) => 
      await _characterFactory.CreatePlayer(WarriorID.Sword, levelData.InitPlayerPos);

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

    private void WarmUp()
    {
      _characterFactory.WarmUp();
      _lootFactory.WarmUp();
      _levelFactory.WarmUp();
      
      _uiFactory.WarmUp();
      _hudFactory.WarmUp();
    }

    private void InitCamera()
    {
      if (Camera.main == null) return;
      
      
      Camera.main.GetComponent<CameraMover>().Follow(_characterRegistry.Player.gameObject);
    }
  }
}
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using WC.Runtime.UI.Services;
using WC.Runtime.Logic.Camera;
using WC.Runtime.Logic.Characters;
using WC.Runtime.Data.Characters;
using WC.Runtime.Gameplay.Services;
using WC.Runtime.StaticData;
using WC.Runtime.UI.Screens;
using Zenject;

namespace WC.Runtime.Infrastructure.Services
{
  public class LoadLevelState : PayloadGameStateBase<DiContainer>
  {
    private readonly ISaveLoadService _saveLoadService;
    private readonly IStaticDataService _staticDataService;
    private readonly IServiceManager _serviceManager;

    private ICharacterFactory _characterFactory;
    private ILevelToolsFactory _levelToolsFactory;
    private IUIFactory _uiFactory;
    private IHUDFactory _hudFactory;
    private ILootFactory _lootFactory;
    private ICharacterInitService _characterInitService;
    private IUIInitService _uiInitService;

    public LoadLevelState(
      IGameStateMachine gameStateMachine,
      ISaveLoadService saveLoadService,
      IStaticDataService staticDataService,
      IServiceManager serviceManager)
    : base(gameStateMachine)
    {
      _saveLoadService = saveLoadService;
      _staticDataService = staticDataService;
      _serviceManager = serviceManager;
    }


    public override async void Enter(DiContainer subContainer, Action onExit = null)
    {
      base.Enter(subContainer, onExit);
      ResolveSubServices(subContainer);
      
      await _serviceManager.WarmUp();
      await CreateEntities();
      _saveLoadService.LoadProgress();
      await InitEntities();

      p_GameStateMachine.Enter<GameLoopState, DiContainer>(subContainer);
    }


    private void ResolveSubServices(DiContainer subContainer)
    {
      _lootFactory = subContainer.Resolve<ILootFactory>();
      _characterFactory = subContainer.Resolve<ICharacterFactory>();
      _levelToolsFactory = subContainer.Resolve<ILevelToolsFactory>();
      _uiFactory = subContainer.Resolve<IUIFactory>();
      _hudFactory = subContainer.Resolve<IHUDFactory>();
      _characterInitService = subContainer.Resolve<ICharacterInitService>();
      _uiInitService = subContainer.Resolve<IUIInitService>();
    }

    private async Task CreateEntities()
    {
      await CreateGameWorld(_staticDataService.Levels[SceneManager.GetActiveScene().name]);
      await CreateUI();
      await CreateHUD();
    }
    
    private async Task InitEntities()
    {
      _characterInitService.DoInit();
      _uiInitService.DoInit();
      // TODO Сделать механику сохранения лута
      //await InitDroppedLoot();
      InitCamera();
    }

    private async Task CreateGameWorld(LevelStaticData levelData)
    {
      await CreateSpawners(levelData);
      await CreatePlayer(levelData);
    }

    private async Task CreateUI()
    {
      await _uiFactory.CreateUI();
      await _uiFactory.Create(UIScreenID.Shop);
    }

    private async Task CreateHUD()
    {
      await _hudFactory.CreateHUD();
    }

    private async Task CreateSpawners(LevelStaticData levelData)
    {
      foreach (EnemySpawnerData spawner in levelData.EnemySpawners)
        await _levelToolsFactory.CreateEnemySpawnPoint(spawner.ID, spawner.Position, spawner.WarriorType);
    }

    private async Task CreatePlayer(LevelStaticData levelData) => 
      await _characterFactory.CreatePlayer(WarriorID.Sword, levelData.StartPlayerPos);

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

    private void InitCamera()
    {
      if (Camera.main == null) return;
      
      Camera.main.GetComponent<CameraMover>().Follow(_characterFactory.Registry.Player.gameObject);
    }
  }
}
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
using WC.Runtime.UI.Elements;
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
      ResolveSubServices(subContainer);
      
      await _serviceManager.WarmUp();
      await CreateGameWorld();
      await CreateUI();
      await CreateHUD();
      InitCamera();
      
      _saveLoadService.LoadProgress();

      base.Enter(subContainer, onExit);
      
      p_GameStateMachine.Enter<GameLoopState, DiContainer>(subContainer);
    }


    private void ResolveSubServices(DiContainer subContainer)
    {
      _characterFactory = subContainer.Resolve<ICharacterFactory>();
      _levelToolsFactory = subContainer.Resolve<ILevelToolsFactory>();
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
      LevelStaticData levelData = _staticDataService.Levels[SceneManager.GetActiveScene().name];
      await CreateSpawners(levelData);
      await CreatePlayer(levelData);

      // TODO Сделать механику сохранения лута
      //await InitDroppedLoot();
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
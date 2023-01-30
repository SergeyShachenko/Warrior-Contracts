using System.Threading.Tasks;
using CodeBase.Data;
using CodeBase.Infrastructure.Factories;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.StaticData;
using CodeBase.Logic.Camera;
using CodeBase.Logic.Characters;
using CodeBase.Logic.Loot;
using CodeBase.Logic.Screens;
using CodeBase.StaticData;
using CodeBase.UI.HUD.Character;
using CodeBase.UI.Services;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.States
{
  public class LoadLevelState : IPayloadState<string>
  {
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly LoadingScreen _loadingScreen;
    private readonly IGameFactory _gameFactory;
    private readonly IPersistentProgressService _progressService;
    private readonly IStaticDataService _staticDataService;
    private readonly IUIFactory _uiFactory;

    public LoadLevelState(GameStateMachine stateMachine,
      SceneLoader sceneLoader,
      LoadingScreen loadingScreen,
      IGameFactory gameFactory,
      IPersistentProgressService progressService, 
      IStaticDataService staticDataService,
      IUIFactory uiFactory)
    {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
      _loadingScreen = loadingScreen;
      _gameFactory = gameFactory;
      _progressService = progressService;
      _staticDataService = staticDataService;
      _uiFactory = uiFactory;
    }

    
    public void Enter(string sceneName)
    {
      _loadingScreen.Show();
      _gameFactory.CleanUp();
      _gameFactory.WarmUp();
      _sceneLoader.Load(sceneName, OnLoaded);
    }

    public void Exit() => 
      _loadingScreen.Hide();

    private async void OnLoaded()
    {
      await InitUI();
      await InitGameWorld();
      
      InformProgressReaders();
      _stateMachine.Enter<GameLoopState>();
    }

    private async Task InitUI() => 
      await _uiFactory.CreateUI();

    private async Task InitGameWorld()
    {
      LevelStaticData levelData = GetLevelStaticData();

      await InitSpawners(levelData);
 
      // TODO Сделать механику сохранения лута
      //await InitDroppedLoot();
      
      GameObject player = await InitPlayer(levelData);
      await InitHUD(player);

      CameraFollow(player);
    }

    private async Task InitSpawners(LevelStaticData levelData)
    {
      foreach (EnemySpawnerData spawner in levelData.EnemySpawners)
        await _gameFactory.CreateSpawnPoint(spawner.ID, spawner.Position, spawner.WarriorType);
    }

    private async Task<GameObject> InitPlayer(LevelStaticData levelData) => 
      await _gameFactory.CreatePlayerWarrior(WarriorType.Sword, levelData.InitPlayerPos);

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

    private async Task InitHUD(GameObject player)
    {
      GameObject hud = await _gameFactory.CreateHUD();
      
      hud.GetComponentInChildren<ActorHUD>()
        .Construct(player.GetComponent<PlayerHealth>());
    }

    private LevelStaticData GetLevelStaticData() => 
      _staticDataService.ForLevel(SceneManager.GetActiveScene().name);

    private void InformProgressReaders()
    {
      foreach (ILoaderProgress progressReader in _gameFactory.ProgressLoaders)
        progressReader.LoadProgress(_progressService.Progress);
    }

    private void CameraFollow(GameObject target) =>
      Camera.main.GetComponent<CameraMover>().Follow(target);
  }
}
using CodeBase.Data;
using CodeBase.Infrastructure.Factories;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.StaticData;
using CodeBase.Logic.Camera;
using CodeBase.Logic.Characters;
using CodeBase.Logic.Screens;
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
      _sceneLoader.Load(sceneName, OnLoaded);
    }

    public void Exit() => 
      _loadingScreen.Hide();

    private void OnLoaded()
    {
      InitUI();
      InitGameWorld();
      InformProgressReaders();
      
      _stateMachine.Enter<GameLoopState>();
    }

    private void InitUI() => 
      _uiFactory.CreateUI();

    private void InitGameWorld()
    {
      LevelStaticData levelData = GetLevelStaticData();
      GameObject player = _gameFactory.CreatePlayer(levelData.InitPlayerPos);
      
      InitSpawners(levelData);
      InitHUD(player);
      CameraFollow(player);
    }

    private void InitSpawners(LevelStaticData levelData)
    {
      foreach (EnemySpawnerData spawner in levelData.EnemySpawners)
        _gameFactory.CreateSpawnPoint(spawner.ID, spawner.Position, spawner.WarriorType);
    }

    private void InitHUD(GameObject player)
    {
      GameObject hud = _gameFactory.CreateHUD();
      hud.GetComponentInChildren<ActorHUD>().Construct(player.GetComponent<PlayerHealth>());
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
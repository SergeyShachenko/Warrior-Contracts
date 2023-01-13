using CodeBase.Infrastructure.Factories;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Logic.Camera;
using CodeBase.Logic.Characters;
using CodeBase.Logic.Screens;
using CodeBase.UI.HUD.Character;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
  public class LoadLevelState : IPayloadState<string>
  {
    private const string SpawnPointTag = "SpawnPoint";
    private const string EnemySpawnerTag = "EnemySpawner";

    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly LoadingScreen _loadingScreen;
    private readonly IGameFactory _gameFactory;
    private readonly IPersistentProgressService _progressService;

    public LoadLevelState(
      GameStateMachine stateMachine, 
      SceneLoader sceneLoader,
      LoadingScreen loadingScreen,
      IGameFactory gameFactory,
      IPersistentProgressService progressService)
    {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
      _loadingScreen = loadingScreen;
      _gameFactory = gameFactory;
      _progressService = progressService;
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
      InitGameWorld();
      InformProgressReaders();
      
      _stateMachine.Enter<GameLoopState>();
    }

    private void InitGameWorld()
    {
      GameObject player = _gameFactory.CreatePlayer(GameObject.FindWithTag(SpawnPointTag));
      InitSpawners();
      InitHUD(player);

      CameraFollow(player);
    }

    private void InitSpawners()
    {
      foreach (GameObject spawnerObj in GameObject.FindGameObjectsWithTag(EnemySpawnerTag))
      {
        var spawner = spawnerObj.GetComponent<EnemySpawner>();
        _gameFactory.Register(spawner);
      }
    }

    private void InitHUD(GameObject player)
    {
      GameObject hud = _gameFactory.CreateHUD();
      hud.GetComponentInChildren<ActorHUD>().Construct(player.GetComponent<PlayerHealth>());
    }

    private void InformProgressReaders()
    {
      foreach (ILoaderProgress progressReader in _gameFactory.ProgressLoaders)
        progressReader.LoadProgress(_progressService.Progress);
    }

    private void CameraFollow(GameObject target) =>
      Camera.main.GetComponent<CameraMover>().Follow(target);
  }
}
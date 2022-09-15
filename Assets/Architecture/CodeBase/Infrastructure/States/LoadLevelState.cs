using CodeBase.Infrastructure.Factories;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Logic.Camera;
using CodeBase.Logic.Hero;
using CodeBase.Logic.Screens;
using CodeBase.UI.HUD.Character;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
  public class LoadLevelState : IPayloadState<string>
  {
    private const string SpawnPointNameTag = "SpawnPoint";

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
      GameObject hero = _gameFactory.CreateHero(GameObject.FindWithTag(SpawnPointNameTag));
      InitHUD(hero);

      CameraFollow(hero);
    }

    private void InitHUD(GameObject hero)
    {
      GameObject hud = _gameFactory.CreateHUD();
      hud.GetComponentInChildren<ActorHUD>().Construct(hero.GetComponent<HeroHealth>());
    }

    private void InformProgressReaders()
    {
      foreach (ILoadProgress progressReader in _gameFactory.ProgressReaders)
        progressReader.LoadProgress(_progressService.Progress);
    }

    private void CameraFollow(GameObject target) =>
      Camera.main.GetComponent<CameraMover>().Follow(target);
  }
}
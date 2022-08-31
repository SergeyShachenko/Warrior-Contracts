using CodeBase.Infrastructure.Factories;
using CodeBase.Infrastructure.States;
using CodeBase.Logic.Camera;
using CodeBase.Logic.Screens;
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

    public LoadLevelState(
      GameStateMachine stateMachine, 
      SceneLoader sceneLoader,
      LoadingScreen loadingScreen,
      IGameFactory gameFactory)
    {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
      _loadingScreen = loadingScreen;
      _gameFactory = gameFactory;
    }

    
    public void Enter(string sceneName)
    {
      _loadingScreen.Show();
      _sceneLoader.Load(sceneName, OnLoaded);
    }

    public void Exit() => 
      _loadingScreen.Hide();

    private void OnLoaded()
    {
      _gameFactory.CreateHUD();
      
      GameObject hero = _gameFactory.CreateHero(GameObject.FindWithTag(SpawnPointNameTag));
      SetCameraFollow(hero);
      
      _stateMachine.Enter<GameLoopState>();
    }

    private void SetCameraFollow(GameObject target) =>
      Camera.main.GetComponent<CameraMover>().Follow(target);
  }
}
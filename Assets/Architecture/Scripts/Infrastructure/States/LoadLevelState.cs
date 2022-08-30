using Architecture.Scripts.Logic.Camera;
using Architecture.Scripts.Logic.Screens;
using Infrastructure.Factories;
using UnityEngine;

namespace Infrastructure.States
{
  public class LoadLevelState : IPayloadState<string>
  {
    private const string SpawnPointNameTag = "SpawnPoint";

    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly LoadingScreen _loadingScreen;
    private readonly IGameFactory _gameFactory;

    public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingScreen loadingScreen)
    {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
      _loadingScreen = loadingScreen;
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
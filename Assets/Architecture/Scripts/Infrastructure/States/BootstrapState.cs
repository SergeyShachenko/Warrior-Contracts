using Services.Input;
using UnityEngine;

namespace Infrastructure.States
{
  public class BootstrapState : IDefaultState
  {
    private const string InitSceneName = "Initial";
    
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;

    public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader)
    {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
    }
    

    public void Enter()
    {
      RegisterServices();
      
      _sceneLoader.Load(InitSceneName, onLoaded: EnterLoadLevel);
    }

    public void Exit()
    {
      
    }

    private void RegisterServices()
    {
      Game.InputService = RegisterInputService();
    }

    private void EnterLoadLevel() => 
      _stateMachine.Enter<LoadLevelState, string>("Level_1");

    private static IInputService RegisterInputService()
    {
      if (Application.isEditor)
        return new StandaloneInputService();
      else
        return new TouchInputService();
    }
  }
}
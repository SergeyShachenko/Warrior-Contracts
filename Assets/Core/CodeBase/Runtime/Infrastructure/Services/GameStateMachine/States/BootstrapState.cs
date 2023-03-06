using System;
using WC.Runtime.Infrastructure.AssetManagement;
using Zenject;

namespace WC.Runtime.Infrastructure.Services
{
  public class BootstrapState : IDefaultState
  {
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    
    private Action _onExit;

    public BootstrapState(GameStateMachine stateMachine, DiContainer container)
    {
      _stateMachine = stateMachine;
      _sceneLoader = container.Resolve<SceneLoader>();
    }


    public void Enter(Action onExit = null)
    {
      _onExit = onExit;
      _sceneLoader.HotLoad(AssetName.Scene.Bootstrap, onLoaded: EnterLoadLevel);
    }

    public void Exit() => 
      _onExit?.Invoke();

    private void EnterLoadLevel() => 
      _stateMachine.Enter<LoadProgressState>();
  }
}
using System;
using WC.Runtime.UI.Screens;
using Zenject;

namespace WC.Runtime.Infrastructure.Services
{
  public class LoadSceneState : PayloadGameStateBase<string>
  {
    private readonly ISceneLoader _sceneLoader;
    private readonly ILoadingScreen _loadingScreen;

    public LoadSceneState(GameStateMachine stateMachine, DiContainer container) : base(stateMachine, container)
    {
      _sceneLoader = Container.Resolve<ISceneLoader>();
      _loadingScreen = container.Resolve<ILoadingScreen>();
    }

    public override void Enter(string sceneName, Action onExit = null)
    {
      _loadingScreen.Show();
      _sceneLoader.HotLoad(sceneName);
      
      base.Enter(sceneName, onExit);
    }
  }
}
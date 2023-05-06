using System;
using WC.Runtime.UI.Screens;

namespace WC.Runtime.Infrastructure.Services
{
  public class LoadSceneState : PayloadGameStateBase<string>
  {
    private readonly ISceneLoader _sceneLoader;
    private readonly ILoadingScreen _loadingScreen;

    public LoadSceneState(
      IGameStateMachine gameStateMachine,
      ISceneLoader sceneLoader, 
      ILoadingScreen loadingScreen)
    : base(gameStateMachine)
    {
      _sceneLoader = sceneLoader;
      _loadingScreen = loadingScreen;
    }

    
    public override void Enter(string sceneName, Action onExit = null)
    {
      _loadingScreen.Show();
      _sceneLoader.Load(sceneName);
      
      base.Enter(sceneName, onExit);
    }
  }
}
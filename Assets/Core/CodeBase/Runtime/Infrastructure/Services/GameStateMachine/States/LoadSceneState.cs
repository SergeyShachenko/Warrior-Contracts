using System;
using WC.Runtime.UI;
using WC.Runtime.UI.Elements;

namespace WC.Runtime.Infrastructure.Services
{
  public class LoadSceneState : PayloadGameStateBase<string>
  {
    private readonly ISceneLoader _sceneLoader;
    private readonly ILoadingScreen _loadingScreen;
    
    private string _sceneName;

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
      _sceneName = sceneName;
      
      SubscribeUpdates();
      _loadingScreen.Show(smoothly: true);

      base.Enter(sceneName, onExit);
    }

    public override void Exit()
    {
      UnsubscribeUpdates();
      
      base.Exit();
    }

    
    private void SubscribeUpdates() => _loadingScreen.ChangeView += OnLoadingScreenVisible;

    private void UnsubscribeUpdates() => _loadingScreen.ChangeView -= OnLoadingScreenVisible;
    
    private void OnLoadingScreenVisible(UIViewType view)
    {
      if (view == UIViewType.Visible) 
        _sceneLoader.Load(_sceneName);
    }
  }
}
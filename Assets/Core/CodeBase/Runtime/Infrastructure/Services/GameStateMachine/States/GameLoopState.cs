using System;
using WC.Runtime.UI.Screens;
using Zenject;

namespace WC.Runtime.Infrastructure.Services
{
  public class GameLoopState : PayloadGameStateBase<DiContainer>
  {
    private readonly IServiceManager _serviceManager;
    private readonly ISaveLoadService _saveLoadService;
    private readonly ILoadingScreen _loadingScreen;

    public GameLoopState(
      IGameStateMachine gameStateMachine,
      IServiceManager serviceManager,
      ISaveLoadService saveLoadService,
      ILoadingScreen loadingScreen)
    : base(gameStateMachine)
    {
      _serviceManager = serviceManager;
      _saveLoadService = saveLoadService;
      _loadingScreen = loadingScreen;
    }


    public override void Enter(DiContainer subContainer, Action onExit = null)
    {
      base.Enter(subContainer, onExit);
      
      ResolveSubServices(subContainer);
      
      _saveLoadService.SaveProgress();
      _loadingScreen.Hide(smoothly: true);
    }

    public override void Exit()
    {
      _serviceManager.CleanUp();
      
      base.Exit();
    }

    
    private void ResolveSubServices(DiContainer subContainer) { }
  }
}
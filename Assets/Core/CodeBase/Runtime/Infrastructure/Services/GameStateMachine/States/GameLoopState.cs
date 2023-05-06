using System;
using Zenject;

namespace WC.Runtime.Infrastructure.Services
{
  public class GameLoopState : PayloadGameStateBase<DiContainer>
  {
    private readonly IServiceManager _serviceManager;
    private readonly ISaveLoadService _saveLoadService;

    public GameLoopState(
      IGameStateMachine gameStateMachine,
      IServiceManager serviceManager,
      ISaveLoadService saveLoadService)
    : base(gameStateMachine)
    {
      _serviceManager = serviceManager;
      _saveLoadService = saveLoadService;
    }


    public override void Enter(DiContainer subContainer, Action onExit = null)
    {
      BindSubServices(subContainer);

      _saveLoadService.SaveProgress();
      
      base.Enter(subContainer, onExit);
    }

    public override void Exit()
    {
      _serviceManager.CleanUp();
      
      base.Exit();
    }

    
    private void BindSubServices(DiContainer subContainer)
    {
      
    }
  }
}
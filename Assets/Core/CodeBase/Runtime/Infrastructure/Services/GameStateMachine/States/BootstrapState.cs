using System;

namespace WC.Runtime.Infrastructure.Services
{
  public class BootstrapState : PayloadGameStateBase<BootstrapConfig>
  {
    private readonly IPersistentProgressService _progressService;
    private readonly ISaveLoadService _saveLoadService;

    public BootstrapState(
      IGameStateMachine gameStateMachine, 
      IPersistentProgressService progressService,
      ISaveLoadService saveLoadService)
    : base(gameStateMachine)
    {
      _progressService = progressService;
      _saveLoadService = saveLoadService;
    }


    public override void Enter(BootstrapConfig bootstrapConfig, Action onExit = null)
    {
      switch (bootstrapConfig.Type)
      {
        case BootstrapType.Default:
        {
          _progressService.Player = _saveLoadService.LoadPlayerProgress();
         
          if (_progressService.Player == null) 
            _progressService.NewProgress();
        }
          break;
        case BootstrapType.Debug:
        {
          _progressService.NewProgress();
        }
          break;
      }
      
      base.Enter(bootstrapConfig, onExit);
      
      p_GameStateMachine.Enter<LoadSceneState, string>(bootstrapConfig.StartScene);
    }
  }
}
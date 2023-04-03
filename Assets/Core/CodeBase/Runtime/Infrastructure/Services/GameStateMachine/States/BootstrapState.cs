using System;
using Zenject;

namespace WC.Runtime.Infrastructure.Services
{
  public class BootstrapState : PayloadGameStateBase<BootstrapConfig>
  {
    private readonly ISaveLoadService _saveLoadService;
    private readonly IPersistentProgressService _progressService;

    public BootstrapState(GameStateMachine stateMachine, DiContainer container) : base(stateMachine, container)
    {
      _progressService = container.Resolve<IPersistentProgressService>();
      _saveLoadService = container.Resolve<ISaveLoadService>();
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
      
      p_StateMachine.Enter<LoadSceneState, string>(bootstrapConfig.StartScene);
    }
  }
}
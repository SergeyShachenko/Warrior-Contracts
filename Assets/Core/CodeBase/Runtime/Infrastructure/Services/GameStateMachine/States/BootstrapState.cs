using System;
using UnityEngine;

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
      base.Enter(bootstrapConfig, onExit);
      
      switch (bootstrapConfig.Type)
      {
        case BootstrapType.Default:
        {
          _progressService.Player = _saveLoadService.LoadPlayerProgress();
         
          if (_progressService.Player == null) 
            _progressService.ResetProgress();
        }
          break;
        case BootstrapType.Debug:
        {
          _progressService.ResetProgress();
        }
          break;
      }

      Application.targetFrameRate = BootstrapMode.FPS;

      p_GameStateMachine.Enter<LoadSceneState, string>(bootstrapConfig.StartScene);
    }
  }
}
using System;
using WC.Runtime.Infrastructure.AssetManagement;
using Zenject;

namespace WC.Runtime.Infrastructure.Services
{
  public class BootstrapState : PayloadGameStateBase<BootstrapType>
  {
    private readonly ISaveLoadService _saveLoadService;
    private readonly IPersistentProgressService _progressService;

    public BootstrapState(GameStateMachine stateMachine, DiContainer container) : base(stateMachine, container)
    {
      _progressService = container.Resolve<IPersistentProgressService>();
      _saveLoadService = container.Resolve<ISaveLoadService>();
    }


    public override void Enter(BootstrapType type, Action onExit = null)
    {
      base.Enter(type, onExit);
      
      switch (type)
      {
        case BootstrapType.Default:
        {
          _progressService.Player = _saveLoadService.LoadPlayerProgress();
         
          if (_progressService.Player == null) 
            _progressService.NewProgress();
          
          StateMachine.Enter<LoadSceneState, string>(AssetName.Scene.MainMenu);
        }
          break;
        case BootstrapType.Level:
        {
          _progressService.NewProgress();
          StateMachine.Enter<LoadSceneState, string>(AssetName.Scene.Level.Flat1);
        }
          break;
      }
    }
  }
}
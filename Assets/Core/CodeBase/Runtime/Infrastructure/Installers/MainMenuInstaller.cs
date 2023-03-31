﻿using WC.Runtime.Infrastructure.Services;
using Zenject;

namespace WC.Runtime.Infrastructure.Installers
{
  public class MainMenuInstaller : MonoInstaller
  {
    private IGameStateMachine _gameStateMachine;

    [Inject]
    private void Construct(IGameStateMachine gameStateMachine) => 
      _gameStateMachine = gameStateMachine;

    public override void InstallBindings()
    {
      if (_gameStateMachine.BootstrapHasOccurred == false) return;
      
      
      _gameStateMachine.Enter<MainMenuState, DiContainer>(Container);
    }
  }
}
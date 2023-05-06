using System;
using WC.Runtime.UI.Screens;
using Zenject;

namespace WC.Runtime.Infrastructure.Services
{
  public class MainMenuState : PayloadGameStateBase<DiContainer>
  {
    private readonly ILoadingScreen _loadingScreen;

    private DiContainer _subContainer;

    public MainMenuState(
      IGameStateMachine gameStateMachine,
      ILoadingScreen loadingScreen)
    : base(gameStateMachine)
    {
      _loadingScreen = loadingScreen;
    }


    public override void Enter(DiContainer subContainer, Action onExit = null)
    {
      _subContainer = subContainer;
      
      _loadingScreen.Hide();
      
      base.Enter(subContainer, onExit);
    }
  }
}
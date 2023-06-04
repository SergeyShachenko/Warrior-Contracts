using System;
using WC.Runtime.UI.Elements;
using Zenject;

namespace WC.Runtime.Infrastructure.Services
{
  public class MainMenuState : PayloadGameStateBase<DiContainer>
  {
    private readonly ILoadingScreen _loadingScreen;

    public MainMenuState(
      IGameStateMachine gameStateMachine,
      ILoadingScreen loadingScreen)
    : base(gameStateMachine)
    {
      _loadingScreen = loadingScreen;
    }


    public override void Enter(DiContainer subContainer, Action onExit = null)
    {
      ResolveSubServices(subContainer);
      _loadingScreen.Hide(smoothly: true);
      
      base.Enter(subContainer, onExit);
    }

    private void ResolveSubServices(DiContainer subContainer) { }
  }
}
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
      base.Enter(subContainer, onExit);
      
      ResolveSubServices(subContainer);
      _loadingScreen.Hide(smoothly: true);
    }

    private void ResolveSubServices(DiContainer subContainer) { }
  }
}
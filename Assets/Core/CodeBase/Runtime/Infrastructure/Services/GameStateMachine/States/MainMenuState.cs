using System;
using WC.Runtime.UI.Screens;
using Zenject;

namespace WC.Runtime.Infrastructure.Services
{
  public class MainMenuState : PayloadGameStateBase<DiContainer>
  {
    private readonly ISceneLoader _sceneLoader;
    private readonly ILoadingScreen _loadingScreen;

    private DiContainer _subContainer;

    public MainMenuState(GameStateMachine stateMachine, DiContainer container) : base(stateMachine, container)
    {
      _sceneLoader = container.Resolve<ISceneLoader>();
      _loadingScreen = container.Resolve<ILoadingScreen>();
    }


    public override void Enter(DiContainer subContainer, Action onExit = null)
    {
      _subContainer = subContainer;
      
      _loadingScreen.Hide();
      
      base.Enter(subContainer, onExit);
    }
  }
}
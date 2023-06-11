using System;
using System.Threading.Tasks;
using WC.Runtime.UI.Screens;
using WC.Runtime.UI.Services;
using Zenject;

namespace WC.Runtime.Infrastructure.Services
{
  public class MainMenuState : PayloadGameStateBase<DiContainer>
  {
    private readonly ILoadingScreen _loadingScreen;
    private readonly IServiceManager _serviceManager;
    
    private IUIFactory _uiFactory;

    public MainMenuState(
      IGameStateMachine gameStateMachine,
      ILoadingScreen loadingScreen,
      IServiceManager serviceManager)
    : base(gameStateMachine)
    {
      _loadingScreen = loadingScreen;
      _serviceManager = serviceManager;
    }


    public override async void Enter(DiContainer subContainer, Action onExit = null)
    {
      base.Enter(subContainer, onExit);
      
      ResolveSubServices(subContainer);
      
      await _serviceManager.WarmUp();
      await CreateUI();

      _loadingScreen.Hide(smoothly: true);
    }
    
    
    private void ResolveSubServices(DiContainer subContainer)
    {
      _uiFactory = subContainer.Resolve<IUIFactory>();
    }

    private async Task CreateUI()
    {
      await _uiFactory.CreateUI();
      await _uiFactory.Create(UIScreenID.Shop);
    }
  }
}
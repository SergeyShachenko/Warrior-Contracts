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
    private readonly ISaveLoadService _saveLoadService;

    private IUIFactory _uiFactory;
    private IUIInitService _uiInitService;

    public MainMenuState(
      IGameStateMachine gameStateMachine,
      ILoadingScreen loadingScreen,
      IServiceManager serviceManager,
      ISaveLoadService saveLoadService)
    : base(gameStateMachine)
    {
      _loadingScreen = loadingScreen;
      _serviceManager = serviceManager;
      _saveLoadService = saveLoadService;
    }


    public override async void Enter(DiContainer subContainer, Action onExit = null)
    {
      base.Enter(subContainer, onExit);
      ResolveSubServices(subContainer);
      
      await _serviceManager.WarmUp();
      await CreateEntities();
      _saveLoadService.LoadProgress();
      await InitEntities();
      
      _loadingScreen.Hide(smoothly: true);
    }

    
    private void ResolveSubServices(DiContainer subContainer)
    {
      _uiFactory = subContainer.Resolve<IUIFactory>();
      _uiInitService = subContainer.Resolve<IUIInitService>();
    }
    
    private async Task CreateEntities()
    {
      await CreateUI();
    }

    private async Task InitEntities()
    {
      _uiInitService.DoInit();
    }

    private async Task CreateUI()
    {
      await _uiFactory.CreateUI();
      await _uiFactory.Create(UIScreenID.Shop);
    }
  }
}
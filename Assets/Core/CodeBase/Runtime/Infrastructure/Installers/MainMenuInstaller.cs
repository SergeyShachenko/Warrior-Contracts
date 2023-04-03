using WC.Runtime.Infrastructure.AssetManagement;
using WC.Runtime.Infrastructure.Services;
using WC.Runtime.UI.Services;
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
      
      
      BindMainServices();
      BindUIServices();
      
      _gameStateMachine.Enter<MainMenuState, DiContainer>(Container);
    }

    
    private void BindMainServices()
    {
      Container
        .Bind<IAssetsProvider>()
        .To<AssetProvider>()
        .AsSingle();
    }

    private void BindUIServices()
    {
      Container
        .Bind<IUIRegistry>()
        .To<UIRegistry>()
        .AsSingle();
      
      Container
        .Bind<IUIFactory>()
        .To<UIFactory>()
        .AsSingle();

      Container
        .Bind<IWindowService>()
        .To<WindowService>()
        .AsSingle();
      
      Container
        .Bind<IPanelService>()
        .To<PanelService>()
        .AsSingle();
    }
  }
}
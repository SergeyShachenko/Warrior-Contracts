using WC.Runtime.Gameplay.Services;
using WC.Runtime.Infrastructure.AssetManagement;
using WC.Runtime.Infrastructure.Services;
using WC.Runtime.UI.Services;
using Zenject;

namespace WC.Runtime.Infrastructure.Installers
{
  public class LevelInstaller : MonoInstaller
  {
    private IGameStateMachine _gameStateMachine;

    [Inject]
    private void Construct(IGameStateMachine gameStateMachine) => 
      _gameStateMachine = gameStateMachine;
    
    
    public override void InstallBindings()
    {
      if (_gameStateMachine.BootstrapHasOccurred == false) return;
      
      
      BindBaseServices();
      BindUIServices();
      BindAdditionalServices();
      
      _gameStateMachine.Enter<LoadLevelState, DiContainer>(Container);
    }
    

    private void BindBaseServices()
    {
      Container
        .Bind<IAssetsProvider>()
        .To<AssetProvider>()
        .AsSingle();
    }

    private void BindUIServices()
    {
      Container
        .Bind<IUIFactory>()
        .To<UIFactory>()
        .AsSingle();
      
      Container
        .Bind<IHUDFactory>()
        .To<HUDFactory>()
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
    
    private void BindAdditionalServices()
    {
      Container
        .Bind<ILootFactory>()
        .To<LootFactory>()
        .AsSingle();

      Container
        .Bind<ICharacterFactory>()
        .To<CharacterFactory>()
        .AsSingle();

      Container
        .Bind<ILevelToolsFactory>()
        .To<LevelToolsFactory>()
        .AsSingle();
    }
  }
}
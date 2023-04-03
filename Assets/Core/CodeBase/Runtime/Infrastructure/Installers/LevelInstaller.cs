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
      
      
      BindMainServices();
      BindUIServices();
      
      _gameStateMachine.Enter<LoadLevelState, DiContainer>(Container);
    }

    
    private void BindMainServices()
    {
      Container
        .Bind<IAssetsProvider>()
        .To<AssetProvider>()
        .AsSingle();

      Container
        .Bind<ILootFactory>()
        .To<LootFactory>()
        .AsSingle();

      Container
        .Bind<ICharacterRegistry>()
        .To<CharacterRegistry>()
        .AsSingle();
      
      Container
        .Bind<ICharacterFactory>()
        .To<CharacterFactory>()
        .AsSingle();

      Container
        .Bind<ILevelFactory>()
        .To<LevelFactory>()
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
  }
}
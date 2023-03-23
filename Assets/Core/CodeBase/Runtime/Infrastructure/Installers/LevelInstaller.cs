using WC.Runtime.Gameplay.Services;
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
      
      
      Container
        .Bind<IHUDFactory>()
        .To<HUDFactory>()
        .AsSingle();
      
      Container
        .Bind<ILootFactory>()
        .To<LootFactory>()
        .AsSingle();

      Container
        .Bind<ICharacterFactory>()
        .To<CharacterFactory>()
        .AsSingle();

      Container
        .Bind<ILevelFactory>()
        .To<LevelFactory>()
        .AsSingle();
      
      _gameStateMachine.Enter<LoadLevelState, DiContainer>(Container);
    }
  }
}
using WC.Runtime.Gameplay.Services;
using WC.Runtime.Infrastructure.Services;
using Zenject;

namespace WC.Runtime.Infrastructure.Installers
{
  public class GameplayLevelInstaller : MonoInstaller
  {
    private IGameStateMachine _gameStateMachine;

    [Inject]
    private void Construct(IGameStateMachine gameStateMachine) => 
      _gameStateMachine = gameStateMachine;
    
    
    public override void InstallBindings()
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

      _gameStateMachine.Enter<LoadLevelState, DiContainer>(Container);
    }
  }
}
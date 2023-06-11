using System;
using WC.Runtime.Gameplay.Services;
using WC.Runtime.Infrastructure.Services;
using WC.Runtime.UI.Services;
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
        .Bind(typeof(IHUDFactory), typeof(IDisposable))
        .To<HUDFactory>()
        .AsSingle();
      
      Container
        .Bind(typeof(ILootFactory), typeof(IDisposable))
        .To<LootFactory>()
        .AsSingle();

      Container
        .Bind(typeof(ICharacterFactory), typeof(IDisposable))
        .To<CharacterFactory>()
        .AsSingle();

      Container
        .Bind(typeof(ILevelToolsFactory), typeof(IDisposable))
        .To<LevelToolsFactory>()
        .AsSingle();

      _gameStateMachine.Enter<LoadLevelState, DiContainer>(Container);
    }
  }
}
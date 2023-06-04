using WC.Runtime.Infrastructure.Services;
using Zenject;

namespace WC.Runtime.Infrastructure.Installers
{
  public class GameStateMachineInstaller : MonoInstaller
  {
    public override void InstallBindings()
    {
      Container
        .Bind<IGameStateMachine>()
        .To<GameStateMachine>()
        .AsSingle()
        .NonLazy();
      
      Container
        .Bind<BootstrapState>()
        .AsSingle()
        .NonLazy();
      
      Container
        .Bind<LoadSceneState>()
        .AsSingle()
        .NonLazy();
      
      Container
        .Bind<MainMenuState>()
        .AsSingle()
        .NonLazy();
      
      Container
        .Bind<LoadLevelState>()
        .AsSingle()
        .NonLazy();
      
      Container
        .Bind<GameLoopState>()
        .AsSingle()
        .NonLazy();
    }
  }
}
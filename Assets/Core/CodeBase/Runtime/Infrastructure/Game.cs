using WC.Runtime.UI.Screens;
using WC.Runtime.Tools;
using WC.Runtime.Infrastructure.Services;
using WC.Runtime.UI;

namespace WC.Runtime.Infrastructure
{
  public class Game
  {
    public GameStateMachine StateMachine;

    public Game(ICoroutineRunner coroutineRunner, LoadingScreen loadingScreen)
    {
      StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), loadingScreen, AllServices.Container);
    }
  }
}
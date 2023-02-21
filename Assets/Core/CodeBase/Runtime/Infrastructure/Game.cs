using CodeBase.Infrastructure.Services;
using CodeBase.Tools;
using CodeBase.UI;

namespace CodeBase.Infrastructure
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
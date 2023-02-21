using CodeBase.Infrastructure.Services;
using CodeBase.Logic.Screens;
using CodeBase.Tools;

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
using Architecture.Scripts.Logic.Screens;
using Infrastructure.Services;
using Infrastructure.Tools;

namespace Infrastructure
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
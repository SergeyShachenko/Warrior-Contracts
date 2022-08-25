using Services.Input;

namespace Infrastructure
{
  public class Game
  {
    public static IInputService InputService;
    public GameStateMachine StateMachine;

    public Game()
    {
      StateMachine = new GameStateMachine();
    }
  }
}
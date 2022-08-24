using Services.Input;
using UnityEngine;

namespace Infrastructure
{
  public class Game
  {
    public static IInputService InputService;


    public Game()
    {
      RegisterInputService();
    }

    private static void RegisterInputService()
    {
      if (Application.isEditor)
        InputService = new DefaultInputService();
      else
        InputService = new TouchInputService();
    }
  }
}
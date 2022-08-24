using UnityEngine;

namespace Infrastructure
{
  public class GameBootLoader : MonoBehaviour
  {
    private Game _game;

    private void Awake()
    {
      _game = new Game();
      
      DontDestroyOnLoad(this);
    }
  }
}
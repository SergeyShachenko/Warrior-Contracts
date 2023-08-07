using UnityEngine;
using WC.Runtime.UI.Elements;
using WC.Runtime.UI.Services;
using Zenject;

namespace WC.Runtime.UI.Elements
{
  public class MainMenuScreen : ScreenBase
  {
    [Header("Buttons")] 
    [SerializeField] private StartGameButton[] _startGameButtons;
    [SerializeField] private OpenUIScreenButton[] _uiScreenButtons;
    
    private IUIFactory _uiFactory;

    [Inject]
    private void Construct(IUIFactory uiFactory) => _uiFactory = uiFactory;


    protected override void Init() => Show();

    protected override void SubscribeUpdates()
    {
      foreach (StartGameButton button in _startGameButtons) 
        button.Pressed += _uiFactory.Registry.UI.StartGame;

      foreach (OpenUIScreenButton button in _uiScreenButtons) 
        button.Pressed += _uiFactory.Registry.UI.Show;
    }

    protected override void UnsubscribeUpdates()
    {
      foreach (StartGameButton button in _startGameButtons) 
        button.Pressed -= _uiFactory.Registry.UI.StartGame;

      foreach (OpenUIScreenButton button in _uiScreenButtons) 
        button.Pressed -= _uiFactory.Registry.UI.Show;
    }
  }
}
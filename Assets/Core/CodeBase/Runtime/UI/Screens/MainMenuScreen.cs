using UnityEngine;
using WC.Runtime.UI.Elements;
using WC.Runtime.UI.Services;
using Zenject;

namespace WC.Runtime.UI.Screens
{
  public class MainMenuScreen : ScreenBase
  {
    [SerializeField] private OpenUIScreenButton _uiScreenButton;
    
    private IUIFactory _uiFactory;

    [Inject]
    private void Construct(IUIFactory uiFactory) => _uiFactory = uiFactory;

    
    protected override void Init() => Show();
    protected override void SubscribeUpdates() => _uiScreenButton.Opened += OnPressedUIScreenButton;
    protected override void UnsubscribeUpdates() => _uiScreenButton.Opened -= OnPressedUIScreenButton;
    protected override void Refresh() { }
    
    
    private void OnPressedUIScreenButton(UIScreenID id) => _uiFactory.Registry.Screens[id].Show(smoothly: true);
  }
}
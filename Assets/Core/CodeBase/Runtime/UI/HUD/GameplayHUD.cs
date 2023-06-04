using UnityEngine;
using WC.Runtime.Gameplay.Services;
using WC.Runtime.Infrastructure.Services;
using WC.Runtime.Logic.Characters;
using WC.Runtime.UI.Elements;
using WC.Runtime.UI.Services;
using Zenject;

namespace WC.Runtime.UI
{
  public class GameplayHUD : MonoBehaviour
  {
    [SerializeField] private HPBar _hpBar;
    [SerializeField] private LootCounter _lootCounter;
    [SerializeField] private OpenWindowButton _openWindowButton;

    private Player _player;
    private IPersistentProgressService _progress;
    private IUIFactory _uiFactory;

    [Inject]
    private void Construct(
      IPersistentProgressService progress,
      ICharacterFactory characterFactory,
      IUIFactory uiFactory)
    {
      _player = characterFactory.Registry.Player;
      _progress = progress;
      _uiFactory = uiFactory;

      _player.Initialized += Init;
    }

    
    private void Init()
    {
      Refresh();

      _player.Health.Changed += RefreshHealthView;
      _progress.Player.World.Loot.Changed += RefreshLootView;
      _openWindowButton.Pressed += OnOpenWindowButtonPressed;
    }

    private void OnDestroy()
    {
      _player.Initialized -= Init;
      _player.Health.Changed -= RefreshHealthView;
      _progress.Player.World.Loot.Changed -= RefreshLootView;
      _openWindowButton.Pressed -= OnOpenWindowButtonPressed;
    }

    
    private void Refresh()
    {
      RefreshHealthView();
      RefreshLootView();
    }
    
    private void RefreshHealthView() => 
      _hpBar.SetProgress(_player.Health.Current, _player.Health.Max);

    private void RefreshLootView() => 
      _lootCounter.Set(_progress.Player.World.Loot.Collected);
    
    private void OnOpenWindowButtonPressed(UIButtonBase button) => 
      _uiFactory.Registry.Windows[_openWindowButton.WindowID].Show(smoothly: true);
  }
}
using UnityEngine;
using WC.Runtime.Gameplay.Services;
using WC.Runtime.Infrastructure.Services;
using WC.Runtime.Logic.Characters;
using WC.Runtime.UI.Character;
using WC.Runtime.UI.Elements;
using WC.Runtime.UI.Screens;
using WC.Runtime.UI.Services;
using Zenject;

namespace WC.Runtime.UI
{
  public class GameplayHUD : ScreenBase
  {
    [SerializeField] private HPBar _hpBar;
    [SerializeField] private LootCounter _lootCounter;
    
    [Header("Buttons")]
    [SerializeField] private OpenUIScreenButton[] _uiScreenButtons;

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
    }


    protected override void Init() => Show();

    protected override void SubscribeUpdates()
    {
      _player.Health.Changed += RefreshHealthView;
      _progress.Player.World.Loot.Changed += RefreshLootView;

      foreach (OpenUIScreenButton button in _uiScreenButtons) 
        button.Pressed += _uiFactory.Registry.UI.Show;
    }

    protected override void UnsubscribeUpdates()
    {
      _player.Health.Changed -= RefreshHealthView;
      _progress.Player.World.Loot.Changed -= RefreshLootView;

      foreach (OpenUIScreenButton button in _uiScreenButtons) 
        button.Pressed -= _uiFactory.Registry.UI.Show;
    }

    protected override void Refresh()
    {
      RefreshHealthView();
      RefreshLootView();
    }

    
    public void RefreshHealthView() => _hpBar.SetProgress(_player.Health.Current, _player.Health.Max);
    public void RefreshLootView() => _lootCounter.Set(_progress.Player.World.Loot.Collected);
  }
}
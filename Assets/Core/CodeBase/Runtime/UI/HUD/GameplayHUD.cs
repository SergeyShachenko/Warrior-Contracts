using UnityEngine;
using WC.Runtime.Gameplay.Services;
using WC.Runtime.Infrastructure.Services;
using WC.Runtime.Logic.Characters;
using WC.Runtime.UI.Elements;
using WC.Runtime.UI.Services;
using WC.Runtime.UI;
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
    private IWindowService _windowService;

    [Inject]
    private void Construct(
      ICharacterRegistry characterRegistry, 
      IPersistentProgressService progress, 
      IWindowService windowService)
    {
      _player = characterRegistry.Player;
      _progress = progress;
      _windowService = windowService;
      
      _player.Initialized += Init;
    }

    
    private void Init()
    {
      UpdateHealthView();
      UpdateLootView();
      
      _player.Health.Changed += UpdateHealthView;
      _progress.Player.World.Loot.Changed += UpdateLootView;
      _openWindowButton.Click += OpenWindowButtonOnClick;
    }

    
    private void OpenWindowButtonOnClick(UIWindowID id) => 
      _windowService.Open(id);

    private void UpdateHealthView() => 
      _hpBar.SetProgress(_player.Health.Current, _player.Health.Max);

    private void UpdateLootView() => 
      _lootCounter.Set(_progress.Player.World.Loot.Collected);
  }
}
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using WC.Runtime.Infrastructure.AssetManagement;
using WC.Runtime.Infrastructure.Services;

namespace WC.Runtime.UI.Screens
{
  public class ShopScreen : ScreenBase
  {
    [SerializeField] private TextMeshProUGUI _moneyLabel;
    [SerializeField] private Button _closeButton;

    [Header("")]
    [SerializeField] private RewardedAdItem _adItem;
    [SerializeField] private ShopItemsContainer _itemsContainer;

    private IPersistentProgressService _progress;

    [Inject]
    private void Construct(
      IAdsService adsService,
      IPersistentProgressService progress,
      IIAPService iapService, 
      IAssetsProvider assetProviderProvider)
    {
      _progress = progress;
      
      _adItem.Construct(adsService, progress);
      _itemsContainer.Construct(iapService, progress, assetProviderProvider);
    }


    protected override void Init() => Refresh();

    protected override void SubscribeUpdates()
    {
      _closeButton.onClick.AddListener(OnCloseButtonPressed);
      _progress.Player.World.Loot.Changed += Refresh;
      _adItem.SubscribeUpdates();
      _itemsContainer.SubscribeUpdates();
    }

    protected override void UnsubscribeUpdates()
    {
      _closeButton.onClick.RemoveListener(OnCloseButtonPressed);
      _progress.Player.World.Loot.Changed -= Refresh;
      _adItem.UnsubscribeUpdates();
      _itemsContainer.UnsubscribeUpdates();
    }

    protected override void Refresh()
    {
      _moneyLabel.text = _progress.Player.World.Loot.Collected.ToString();
      _adItem.Refresh();
      _itemsContainer.Refresh();
    }

    private void OnCloseButtonPressed() => Hide(smoothly: true);
  }
}
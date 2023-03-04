using TMPro;
using UnityEngine;
using WC.Runtime.Infrastructure.AssetManagement;
using WC.Runtime.Infrastructure.Services;

namespace WC.Runtime.UI
{
  public class ShopWindow : WindowBase
  {
    [SerializeField] private TextMeshProUGUI _moneyLabel;
    [SerializeField] private RewardedAdItem _adItem;
    [SerializeField] private ShopItemsContainer _itemsContainer;

    private IPersistentProgressService _progress;

    public void Construct(
      IAdsService adsService,
      IPersistentProgressService progress,
      IIAPService iapService, 
      IAssetsProvider assetProviderProvider)
    {
      _progress = progress;
      
      _adItem.Construct(adsService, progress);
      _itemsContainer.Construct(iapService, progress, assetProviderProvider);
      
      Init();
    }
    
    protected override void Init()
    {
      base.Init();
      
      _adItem.Init();
      _itemsContainer.Init();
      UpdateMoneyLabel();
    }

    
    protected override void SubscribeUpdates()
    {
      _progress.Player.World.Loot.Changed += UpdateMoneyLabel;
      _adItem.Subscribe();
      _itemsContainer.Subscribe();
    }

    protected override void CleanUp()
    {
      base.CleanUp();
      
      _progress.Player.World.Loot.Changed -= UpdateMoneyLabel;
      _adItem.CleanUp();
      _itemsContainer.CleanUp();
    }

    private void UpdateMoneyLabel() => 
      _moneyLabel.text = _progress.Player.World.Loot.Collected.ToString();
  }
}
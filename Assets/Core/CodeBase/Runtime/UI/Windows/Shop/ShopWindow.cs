using TMPro;
using UnityEngine;
using WC.Runtime.Infrastructure.AssetManagement;
using WC.Runtime.Infrastructure.Services;

namespace WC.Runtime.UI.Windows
{
  public class ShopWindow : WindowBase
  {
    [SerializeField] private TextMeshProUGUI _moneyLabel;
    [SerializeField] private RewardedAdItem _adItem;
    [SerializeField] private ShopItemsContainer _itemsContainer;

    public void Construct(
      IAdsService adsService,
      IPersistentProgressService progressService,
      IIAPService iapService, 
      IAssetsProvider assetProviderProvider)
    {
      base.Construct(progressService);
      
      _adItem.Construct(adsService, progressService);
      _itemsContainer.Construct(iapService, progressService, assetProviderProvider);
    }
    
    protected override void Init()
    {
      _adItem.Init();
      _itemsContainer.Init();
      UpdateMoneyLabel();
    }

    
    protected override void SubscribeUpdates()
    {
      p_PlayerProgress.World.Loot.Changed += UpdateMoneyLabel;
      _adItem.Subscribe();
      _itemsContainer.Subscribe();
    }

    protected override void CleanUp()
    {
      base.CleanUp();
      
      p_PlayerProgress.World.Loot.Changed -= UpdateMoneyLabel;
      _adItem.CleanUp();
      _itemsContainer.CleanUp();
    }

    private void UpdateMoneyLabel() => 
      _moneyLabel.text = p_PlayerProgress.World.Loot.Collected.ToString();
  }
}
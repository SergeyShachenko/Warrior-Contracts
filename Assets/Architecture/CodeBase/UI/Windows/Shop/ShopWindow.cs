using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using TMPro;
using UnityEngine;

namespace CodeBase.UI
{
  public class ShopWindow : WindowBase
  {
    [SerializeField] private TextMeshProUGUI _moneyLabel;
    [SerializeField] private RewardedAdItem _adItem;

    public void Construct(IAdsService adsService, IPersistentProgressService progressService)
    {
      base.Construct(progressService);
      
      _adItem.Construct(adsService, progressService);
    }
    
    protected override void Init()
    {
      _adItem.Init();
      UpdateMoneyLabel();
    }

    
    protected override void SubscribeUpdates()
    {
      p_PlayerProgress.World.Loot.Changed += UpdateMoneyLabel;
      _adItem.Subscribe();
    }

    protected override void CleanUp()
    {
      base.CleanUp();
      
      p_PlayerProgress.World.Loot.Changed -= UpdateMoneyLabel;
      _adItem.CleanUp();
    }

    private void UpdateMoneyLabel() => 
      _moneyLabel.text = p_PlayerProgress.World.Loot.Collected.ToString();
  }
}
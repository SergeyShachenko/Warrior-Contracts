using TMPro;

namespace CodeBase.UI
{
  public class ShopWindow : WindowBase
  {
    public TextMeshProUGUI MoneyLabel;

    protected override void Init() => 
      UpdateMoneyLabel();

    protected override void SubscribeUpdates() => 
      p_PlayerProgress.World.AllLoot.Changed += UpdateMoneyLabel;

    protected override void CleanUp()
    {
      base.CleanUp();
      
      p_PlayerProgress.World.AllLoot.Changed -= UpdateMoneyLabel;
    }

    private void UpdateMoneyLabel() => 
      MoneyLabel.text = p_PlayerProgress.World.AllLoot.Collected.ToString();
  }
}
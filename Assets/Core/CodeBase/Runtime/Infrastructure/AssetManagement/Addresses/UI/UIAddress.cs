namespace WC.Runtime.Infrastructure.AssetManagement
{
  public class UIAddress
  {
    public string MainUI => "UI_Main";
    
    public HUDAddress HUD { get; } = new();
    public UIWindowAddress Window { get; } = new();
    public UIPanelAddress Panel { get; } = new();
  }
}
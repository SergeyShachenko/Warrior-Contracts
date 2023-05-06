namespace WC.Runtime.Infrastructure.AssetManagement
{
  public class UIAddress
  {
    public string MainUI => "UI_Main";
    
    public HUDAddress HUD => new();
    public UIScreenAddress Screen => new();
    public UIWindowAddress Window => new();
  }
}
namespace WC.Runtime.Infrastructure.AssetManagement
{
  public class HUDAddress
  {
    public string GameplayHUD => "HUD_Gameplay";
    
    public HUDWindowAddress Windows => new();
    public HUDScreenAddress Screens => new();
  }
}
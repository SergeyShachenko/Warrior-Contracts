namespace WC.Runtime.UI.Screens
{
  public class LoadingScreen : ScreenBase,
    ILoadingScreen
  {
    public override void Show()
    {
      p_Canvas.enabled = true;
      p_CanvasGroup.alpha = 1f;
    }
  }
}
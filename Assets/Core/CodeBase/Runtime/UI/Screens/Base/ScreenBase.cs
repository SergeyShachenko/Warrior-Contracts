namespace WC.Runtime.UI.Screens
{
  public abstract class ScreenBase : UIElementBase
  {
    public override void Show(bool smoothly = false)
    {
      base.Show(smoothly);
      Refresh();
    }

    protected abstract void Refresh();
  }
}
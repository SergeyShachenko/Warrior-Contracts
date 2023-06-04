namespace WC.Runtime.UI.Elements
{
  public abstract class WindowBase : UIElementBase
  {
    public override void Show(bool smoothly = false)
    {
      base.Show(smoothly);
      Refresh();
    }

    protected abstract void Refresh();
  }
}
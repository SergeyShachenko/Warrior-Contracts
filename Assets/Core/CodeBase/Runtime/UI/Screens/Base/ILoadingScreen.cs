using System;

namespace WC.Runtime.UI.Screens
{
  public interface ILoadingScreen
  {
    event Action<UIViewType> ChangeView;
    
    void Show(bool smoothly = false);
    void Hide(bool smoothly = false);
  }
}
using System;
using WC.Runtime.UI.Windows;
using WC.Runtime.UI;

namespace WC.Runtime.Data
{
  [Serializable]
  public class WindowConfig
  {
    public WindowID ID;
    public WindowBase Window;
  }
}
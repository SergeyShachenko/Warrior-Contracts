using UnityEngine;

namespace WC.Runtime.UI.Elements
{
  public class OpenWindowButton : UIButtonBase
  {
    [field: SerializeField] public UIWindowID WindowID { get; private set; }
  }
}
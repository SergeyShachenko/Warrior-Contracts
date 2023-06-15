using System;
using UnityEngine;

namespace WC.Runtime.UI.Elements
{
  public class OpenUIWindowButton : UIButtonBase
  {
    public event Action<UIWindowID> Pressed;
    
    [field: SerializeField] public UIWindowID ID { get; private set; }
    
    
    protected override void OnPressed()
    {
      base.OnPressed();
      Pressed?.Invoke(ID);
    }
  }
}
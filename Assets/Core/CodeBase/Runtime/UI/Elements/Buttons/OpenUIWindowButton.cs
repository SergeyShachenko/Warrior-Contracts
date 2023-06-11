using System;
using UnityEngine;

namespace WC.Runtime.UI.Elements
{
  public class OpenUIWindowButton : UIButtonBase
  {
    public event Action<UIWindowID> Opened;
    
    [field: SerializeField] public UIWindowID ID { get; private set; }
    
    
    protected override void OnPressed()
    {
      base.OnPressed();
      Opened?.Invoke(ID);
    }
  }
}
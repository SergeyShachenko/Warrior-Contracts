using System;
using UnityEngine;
using WC.Runtime.UI.Elements;

namespace WC.Runtime.UI.Elements
{
  public class OpenUIScreenButton : UIButtonBase
  {
    public event Action<UIScreenID> Pressed; 

    [field: SerializeField] public UIScreenID ID { get; private set; }

    
    protected override void OnPressed()
    {
      base.OnPressed();
      Pressed?.Invoke(ID);
    }
  }
}
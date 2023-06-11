﻿using System;
using UnityEngine;
using WC.Runtime.UI.Screens;

namespace WC.Runtime.UI.Elements
{
  public class OpenUIScreenButton : UIButtonBase
  {
    public event Action<UIScreenID> Opened; 

    [field: SerializeField] public UIScreenID ID { get; private set; }

    
    protected override void OnPressed()
    {
      base.OnPressed();
      Opened?.Invoke(ID);
    }
  }
}
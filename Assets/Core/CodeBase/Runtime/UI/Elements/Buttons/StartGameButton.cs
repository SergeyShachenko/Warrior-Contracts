using System;
using UnityEngine;

namespace WC.Runtime.UI.Elements
{
  public class StartGameButton : UIButtonBase
  {
    public event Action<StartGameType> Pressed; 

    [SerializeField] private StartGameType _type;


    protected override void OnPressed()
    {
      base.OnPressed();
      Pressed?.Invoke(_type);
    }
  }
}
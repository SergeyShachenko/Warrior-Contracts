using System;
using UnityEngine;

namespace WC.Runtime.UI.Elements
{
  public class LoadSceneButton : UIButtonBase
  {
    public event Action<string> Pressed; 
    
    [SerializeField] private string _scene;


    protected override void OnPressed()
    {
      base.OnPressed();
      Pressed?.Invoke(_scene);
    }
  }
}
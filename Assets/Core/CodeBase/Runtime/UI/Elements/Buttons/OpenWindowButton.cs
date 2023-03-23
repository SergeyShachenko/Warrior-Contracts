using System;
using UnityEngine;
using UnityEngine.UI;

namespace WC.Runtime.UI.Elements
{
  public class OpenWindowButton : MonoBehaviour
  {
    //TODO Разобраться с логикой
    public event Action<UIWindowID> Click;
    
    [SerializeField] private UIWindowID _windowID;
    
    [Header("Links")]
    [SerializeField] private Button _button;
    

    private void Awake() => 
      _button.onClick.AddListener(() => Click?.Invoke(_windowID));
  }
}
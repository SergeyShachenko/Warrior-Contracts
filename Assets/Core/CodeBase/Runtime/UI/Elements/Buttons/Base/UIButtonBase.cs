using System;
using UnityEngine;
using UnityEngine.UI;

namespace WC.Runtime.UI.Elements
{
  public abstract class UIButtonBase : MonoBehaviour
  {
    public event Action<UIButtonBase> Pressed; 
    
    [SerializeField] private Button _button;


    private void Awake()
    {
      Init();
      SubscribeUpdates();
    }

    private void OnDestroy() => UnsubscribeUpdates();
    
    
    public virtual void Enable() => _button.interactable = true;
    public virtual void Disable() => _button.interactable = false;
    
    protected virtual void Init(){}
    protected virtual void SubscribeUpdates() => _button.onClick.AddListener(OnPressed);
    protected virtual void UnsubscribeUpdates() => _button.onClick.RemoveListener(OnPressed);
    protected virtual void OnPressed() => Pressed?.Invoke(this); 
  }
}
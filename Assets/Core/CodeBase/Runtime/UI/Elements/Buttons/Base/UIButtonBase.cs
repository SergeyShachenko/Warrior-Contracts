using UnityEngine;
using UnityEngine.UI;

namespace WC.Runtime.UI.Elements
{
  public abstract class UIButtonBase : MonoBehaviour
  {
    [SerializeField] private Button _button;


    private void Awake()
    {
      Init();
      SubscribeUpdates();
    }

    private void OnDestroy() => UnsubscribeUpdates();

    
    protected virtual void Init(){}
    protected virtual void SubscribeUpdates() => _button.onClick.AddListener(OnPressed);
    protected virtual void UnsubscribeUpdates() => _button.onClick.RemoveListener(OnPressed);
    protected virtual void OnPressed(){}
    
    
    public virtual void Enable() => _button.interactable = true;
    public virtual void Disable() => _button.interactable = false;
  }
}
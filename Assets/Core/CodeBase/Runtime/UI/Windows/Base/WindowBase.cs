using UnityEngine;
using UnityEngine.UI;

namespace WC.Runtime.UI
{
  public abstract class WindowBase : MonoBehaviour
  {
    public bool IsVisible
    {
      get => _isVisible;
      
      private set
      {
        if (value == _isVisible) return;
        
        
        foreach (Canvas canvas in _canvases) 
          canvas.enabled = value;

        _isVisible = value;
      }
    }

    [SerializeField] private Canvas[] _canvases;
    [SerializeField] private Button _closeButton;

    private bool _isVisible = true;

    protected virtual void Init()
    {
      SubscribeUpdates();
      Close();
      
      _closeButton.onClick.AddListener(Close);
    }

    
    protected void OnDestroy() => 
      CleanUp();


    public void Open() => 
      IsVisible = true;

    public void Close() =>
      IsVisible = false;
    
    protected virtual void SubscribeUpdates(){}
    protected virtual void CleanUp(){}
  }
}
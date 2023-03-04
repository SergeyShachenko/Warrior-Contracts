using UnityEngine;

namespace WC.Runtime.UI
{
  public class PanelBase : MonoBehaviour
  {
    public bool IsVisible
    {
      get => _canvas.enabled;
      private set => _canvas.enabled = value;
    }
    
    [SerializeField] private Canvas _canvas;


    protected void Awake() => 
      Hide();

    protected void Start()
    {
      Init();
      SubscribeUpdates();
    }

    protected void OnDestroy() => 
      CleanUp();
    
    
    public void Show() => 
      IsVisible = true;

    public void Hide() =>
      IsVisible = false;
    
    protected virtual void Init(){}
    protected virtual void SubscribeUpdates(){}
    protected virtual void CleanUp(){}
  }
}
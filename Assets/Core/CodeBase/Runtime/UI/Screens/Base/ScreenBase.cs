using System.Collections;
using UnityEngine;

namespace WC.Runtime.UI
{
  public abstract class ScreenBase : MonoBehaviour
  {
    public bool IsVisible => _canvas.enabled;

    protected Canvas p_Canvas => _canvas;
    protected CanvasGroup p_CanvasGroup => _canvasGroup;

    [SerializeField] private float _fadeStep = 0.05f;

    [Header("Links")]
    [SerializeField] private Canvas _canvas;
    [SerializeField] private CanvasGroup _canvasGroup;
    
    private readonly WaitForSeconds _fadeDelay = new(0.0001f);


    protected void Awake()
    {
      _canvas.enabled = false;
      _canvasGroup.alpha = 0f;
    }

    protected void Start()
    {
      Init();
      SubscribeUpdates();
    }

    protected void OnDestroy() => CleanUp();
    
    
    public virtual void Show()
    {
      StopAllCoroutines();
      StartCoroutine(FadeIn());
    }

    public virtual void Hide()
    {
      StopAllCoroutines();
      StartCoroutine(FadeOut());
    }

    protected virtual void Init(){}
    protected virtual void SubscribeUpdates(){}
    protected virtual void CleanUp(){}

    private IEnumerator FadeIn()
    {
      _canvas.enabled = true;
      
      while (_canvasGroup.alpha < 1)
      {
        _canvasGroup.alpha += _fadeStep;
        yield return _fadeDelay;
      }
    }
    
    private IEnumerator FadeOut()
    {
      while (_canvasGroup.alpha > 0)
      {
        _canvasGroup.alpha -= _fadeStep;
        yield return _fadeDelay;
      }
      
      _canvas.enabled = false;
    }
  }
}
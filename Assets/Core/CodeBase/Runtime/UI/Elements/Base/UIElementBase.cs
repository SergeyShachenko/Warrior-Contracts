using System;
using System.Collections;
using UnityEngine;

namespace WC.Runtime.UI
{
  public abstract class UIElementBase : MonoBehaviour
  {
    public event Action<UIViewType> ChangeView;

    public UIViewType CurrentView { get; private set; } = UIViewType.None;
    
    [Header("Fade Settings")] 
    [SerializeField] private float _fadeInDuration = 0.15f;
    [SerializeField] private float _fadeOutDuration = 0.15f;

    [Header("Links")] 
    [SerializeField] private Canvas[] _canvases;
    [SerializeField] private CanvasGroup _canvasGroup;

    private Coroutine _transitionCoroutine;

    
    private void Awake()
    {
      Init();
      SubscribeUpdates();
    }

    private void OnDestroy() => UnsubscribeUpdates();

    
    protected abstract void Init();
    protected abstract void SubscribeUpdates();
    protected abstract void UnsubscribeUpdates();

    
    public virtual void Show(bool smoothly = false)
    {
      if (CurrentView == UIViewType.Visible) return;


      if (_transitionCoroutine != null)
        StopCoroutine(_transitionCoroutine);

      if (smoothly == false)
        SetView(UIViewType.Visible);
      else
        _transitionCoroutine = StartCoroutine(FadeIn(_fadeInDuration));
    }

    public virtual void Hide(bool smoothly = false)
    {
      if (CurrentView == UIViewType.Hidden) return;

      
      if (_transitionCoroutine != null)
        StopCoroutine(_transitionCoroutine);

      if (smoothly == false)
        SetView(UIViewType.Hidden);
      else
        _transitionCoroutine = StartCoroutine(FadeOut(_fadeOutDuration));
    }

    public virtual void SwitchView(bool smoothly = false)
    {
      switch (CurrentView)
      {
        case UIViewType.Hidden: Show(smoothly); break;
        case UIViewType.Visible: Hide(smoothly); break;
      }
    }

    
    private IEnumerator FadeIn(float duration)
    {
      SetView(UIViewType.Transitional);
      
      float startTime = Time.time;
      
      while (Time.time - startTime < duration)
      {
        _canvasGroup.alpha = Mathf.Clamp01((Time.time - startTime) / duration);
        yield return null;
      }

      _canvasGroup.alpha = 1;
      SetView(UIViewType.Visible);
    }

    private IEnumerator FadeOut(float duration)
    {
      SetView(UIViewType.Transitional);
      float startTime = Time.time;
      
      while (Time.time - startTime < duration)
      {
        _canvasGroup.alpha = Mathf.Clamp01(1 - (Time.time - startTime) / duration);
        yield return null;
      }

      _canvasGroup.alpha = 0;
      SetView(UIViewType.Hidden);
    }

    private void SetView(UIViewType view)
    {
      switch (view)
      {
        case UIViewType.Visible:
        {
          foreach (Canvas canvas in _canvases)
            canvas.enabled = true;
          
          _canvasGroup.alpha = 1;
        }
          break;

        case UIViewType.Transitional:
        {
          foreach (Canvas canvas in _canvases)
            canvas.enabled = true;
        }
          break;

        case UIViewType.Hidden:
        {
          foreach (Canvas canvas in _canvases)
            canvas.enabled = false;
          
          _canvasGroup.alpha = 0;
        }
          break;
      }

      CurrentView = view;

      if (view != UIViewType.Transitional)
        ChangeView?.Invoke(view);
    }
  }
}
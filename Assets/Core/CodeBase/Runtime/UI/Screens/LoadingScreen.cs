using System.Collections;
using UnityEngine;

namespace WC.Runtime.UI.Screens
{
  public class LoadingScreen : MonoBehaviour,
    IScreen
  {
    private readonly WaitForSeconds _waitDelay = new(0.000001f);
    
    [SerializeField] private Canvas _canvas;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private float _fadeInSpeed = 0.05f;


    private void Awake()
    {
      _canvas.enabled = false;
      _canvasGroup.alpha = 0f;
    }

    
    public void Show()
    {
      _canvas.enabled = true;
      _canvasGroup.alpha = 1f;
    }

    public void Hide() =>
      StartCoroutine(FadeIn());

    private IEnumerator FadeIn()
    {
      while (_canvasGroup.alpha > 0)
      {
        _canvasGroup.alpha -= _fadeInSpeed;
        yield return _waitDelay;
      }
      
      _canvas.enabled = false;
    }
  }
}
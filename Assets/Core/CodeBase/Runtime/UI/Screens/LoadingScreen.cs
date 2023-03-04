using System.Collections;
using UnityEngine;

namespace WC.Runtime.UI.Screens
{
  public class LoadingScreen : MonoBehaviour,
    ILoadingScreen
  {
    private readonly WaitForSeconds _waitDelay = new(0.0001f);
    private readonly WaitForSeconds _fadeOutDelay = new(1f);
    
    [SerializeField] private Canvas _canvas;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private float _fadeOutSpeed = 0.05f;


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
      StartCoroutine(FadeOut());

    private IEnumerator FadeOut()
    {
      yield return _fadeOutDelay;
      
      while (_canvasGroup.alpha > 0)
      {
        _canvasGroup.alpha -= _fadeOutSpeed;
        yield return _waitDelay;
      }
      
      _canvas.enabled = false;
    }
  }
}
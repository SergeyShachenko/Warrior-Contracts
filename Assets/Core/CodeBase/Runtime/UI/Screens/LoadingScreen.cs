using System.Collections;
using UnityEngine;

namespace CodeBase.UI
{
  public class LoadingScreen : MonoBehaviour,
    IScreen
  {
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private float _fadeInSpeed = 0.03f;
    

    private void Awake()
    {
      DontDestroyOnLoad(this);
    }


    public void Show()
    {
      gameObject.SetActive(true);
      _canvasGroup.alpha = 1f;
    }

    public void Hide() =>
      StartCoroutine(FadeIn());

    private IEnumerator FadeIn()
    {
      while (_canvasGroup.alpha > 0)
      {
        _canvasGroup.alpha -= _fadeInSpeed;
        yield return new WaitForSeconds(_fadeInSpeed);
      }
      
      gameObject.SetActive(false);
    }
  }
}
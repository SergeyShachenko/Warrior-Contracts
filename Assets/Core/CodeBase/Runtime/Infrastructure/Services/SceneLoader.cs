using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using WC.Runtime.Tools;
using WC.Runtime.UI.Screens;

namespace WC.Runtime.Infrastructure
{
  public class SceneLoader
  {
    private readonly ICoroutineRunner _coroutineRunner;
    private readonly LoadingScreen _loadingScreen;
    
    public SceneLoader(ICoroutineRunner coroutineRunner, LoadingScreen loadingScreen)
    {
      _coroutineRunner = coroutineRunner;
      _loadingScreen = loadingScreen;
    }


    public void Load(string name, Action onLoaded = null) => 
      _coroutineRunner.StartCoroutine(LoadScene(hotLoad: false, name, onLoaded));

    public void HotLoad(string name, Action onLoaded = null)
    {
      _loadingScreen.Show();
      _coroutineRunner.StartCoroutine(LoadScene(hotLoad: true, name, onLoaded));
    }
    
    public void ReloadCurrent(Action onReload = null) => 
      _coroutineRunner.StartCoroutine(LoadScene(hotLoad: false, SceneManager.GetActiveScene().name, onReload));
    
    public void HotReloadCurrent(Action onReload = null)
    {
      _loadingScreen.Show();
      _coroutineRunner.StartCoroutine(LoadScene(hotLoad: true, SceneManager.GetActiveScene().name, onReload));
    }

    private IEnumerator LoadScene(bool hotLoad, string nextScene, Action onLoaded = null)
    {
      if (SceneManager.GetActiveScene().name == nextScene)
      {
        onLoaded?.Invoke();
        
        yield break;
      }

      AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene);
      
      while (waitNextScene.isDone == false)
        yield return null;

      onLoaded?.Invoke();
      
      if (hotLoad) 
        _loadingScreen.Hide();
    }
  }
}
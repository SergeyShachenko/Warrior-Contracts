using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using WC.Runtime.Infrastructure.Services;
using WC.Runtime.UI.Screens;

namespace WC.Runtime.Infrastructure
{
  public class SceneLoader : ISceneLoader
  {
    private readonly ICoroutineRunner _coroutineRunner;
    private readonly ILoadingScreen _loadingScreen;
    
    public SceneLoader(ICoroutineRunner coroutineRunner, ILoadingScreen loadingScreen)
    {
      _coroutineRunner = coroutineRunner;
      _loadingScreen = loadingScreen;
    }


    public void Load(string name, bool withLoadingScreen = false, Action onLoaded = null) => 
      _coroutineRunner.StartCoroutine(LoadScene(name, withLoadingScreen, onLoaded));

    public void ReloadCurrent(bool withLoadingScreen = false, Action onReload = null) => 
      _coroutineRunner.StartCoroutine(LoadScene(SceneManager.GetActiveScene().name, withLoadingScreen, onReload));

    private IEnumerator LoadScene(string nextScene, bool withCurtain, Action onLoaded = null)
    {
      if (withCurtain) 
        _loadingScreen.Show();
      
      AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene);
      
      while (waitNextScene.isDone == false)
        yield return null;

      onLoaded?.Invoke();
      
      if (withCurtain) 
        _loadingScreen.Hide();
    }
  }
}
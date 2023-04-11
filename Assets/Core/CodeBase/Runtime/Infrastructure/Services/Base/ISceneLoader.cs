using System;

namespace WC.Runtime.Infrastructure.Services
{
  public interface ISceneLoader
  {
    void Load(string name, bool withLoadingScreen = false, Action onLoaded = null);
    void ReloadCurrent(bool withLoadingScreen = false, Action onReload = null);
  }
}
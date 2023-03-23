using System;

namespace WC.Runtime.Infrastructure.Services
{
  public interface ISceneLoader
  {
    void Load(string name, Action onLoaded = null);
    void HotLoad(string name, Action onLoaded = null);
    void ReloadCurrent(Action onReload = null);
    void HotReloadCurrent(Action onReload = null);
  }
}
using System;
using WC.Runtime.UI.Services;
using Zenject;

namespace WC.Runtime.Infrastructure.Installers
{
  public class UIServicesInstaller : MonoInstaller
  {
    public override void InstallBindings()
    {
      Container
        .Bind(typeof(IUIFactory), typeof(IDisposable))
        .To<UIFactory>()
        .AsSingle();
      
      Container
        .Bind(typeof(IHUDFactory), typeof(IDisposable))
        .To<HUDFactory>()
        .AsSingle();
    }
  }
}
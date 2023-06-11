using System;
using WC.Runtime.Infrastructure.AssetManagement;
using WC.Runtime.UI.Services;
using Zenject;

namespace WC.Runtime.Infrastructure.Installers
{
  public class BaseServicesInstaller : MonoInstaller
  {
    public override void InstallBindings()
    {
      Container
        .Bind<IAssetsProvider>()
        .To<AssetProvider>()
        .AsSingle();
      
      Container
        .Bind(typeof(IUIFactory), typeof(IDisposable))
        .To<UIFactory>()
        .AsSingle();
    }
  }
}
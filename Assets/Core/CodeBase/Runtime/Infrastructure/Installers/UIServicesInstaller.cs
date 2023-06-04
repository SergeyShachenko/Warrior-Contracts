using WC.Runtime.UI.Services;
using Zenject;

namespace WC.Runtime.Infrastructure.Installers
{
  public class UIServicesInstaller : MonoInstaller
  {
    public override void InstallBindings()
    {
      Container
        .Bind<IUIFactory>()
        .To<UIFactory>()
        .AsSingle();
      
      Container
        .Bind<IHUDFactory>()
        .To<HUDFactory>()
        .AsSingle();
    }
  }
}
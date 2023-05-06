using WC.Runtime.Infrastructure.AssetManagement;
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
    }
  }
}
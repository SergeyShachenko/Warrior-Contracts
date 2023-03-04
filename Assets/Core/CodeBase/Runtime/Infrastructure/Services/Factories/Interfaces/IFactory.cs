using System.Collections.Generic;
using System.Threading.Tasks;

namespace WC.Runtime.Infrastructure.Services
{
  public interface IFactory
  {
    List<ISaverProgress> ProgressSavers { get; }
    List<ILoaderProgress> ProgressLoaders { get; }

    void Register(ILoaderProgress progressLoader);

    Task WarmUp();
    void CleanUp();
  }
}
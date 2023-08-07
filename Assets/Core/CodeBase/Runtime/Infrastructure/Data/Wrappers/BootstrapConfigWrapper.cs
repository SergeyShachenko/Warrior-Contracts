using System;
using WC.Runtime.Infrastructure;

namespace WC.Runtime.Infrastructure.Data
{
  [Serializable]
  public class BootstrapConfigWrapper
  {
    public BootstrapType BootstrapMode;
    public int FPS;
  }
}
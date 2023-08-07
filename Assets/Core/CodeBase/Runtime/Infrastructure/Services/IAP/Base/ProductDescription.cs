using UnityEngine.Purchasing;
using WC.Runtime.Infrastructure.Data.IAP;

namespace WC.Runtime.Infrastructure.Services
{
  public struct ProductDescription
  {
    public string ID;
    public Product Product;
    public ProductConfig Config;
    public int AvailablePurchasesLeft;
  }
}
using UnityEngine.Purchasing;

namespace WC.Runtime.Infrastructure.Services
{
  public class ProductDescription
  {
    public string ID;
    public Product Product;
    public ProductConfig Config;
    public int AvailablePurchasesLeft;
  }
}
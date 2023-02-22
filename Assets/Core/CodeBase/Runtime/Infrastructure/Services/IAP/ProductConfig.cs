using System;
using UnityEngine.Purchasing;

namespace WC.Runtime.Infrastructure.Services
{
  [Serializable]
  public class ProductConfig
  {
    public string ID;
    public ProductType ProductType;
    public ItemType ItemType;
    
    public int MaxPurchaseCount;
    public int Amount;
    public string Price;
    
    public string IconAddress;
  }
}
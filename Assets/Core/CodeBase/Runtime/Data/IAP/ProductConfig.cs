using System;
using UnityEngine.Purchasing;
using WC.Runtime.Infrastructure.Services;

namespace WC.Runtime.Data.IAP
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
using System;
using System.Collections.Generic;
using UnityEngine.Purchasing;
using WC.Runtime.Infrastructure.Services;

namespace WC.Runtime.Infrastructure.Services
{
  public interface IIAPService : IService
  {
    event Action Initizlized;
    bool IsInitialized { get; }
    void Init();
    List<ProductDescription> GetProductDescriptions();
    void StartPurchase(string productID);
    PurchaseProcessingResult ProcessPurchase(Product product);
  }
}
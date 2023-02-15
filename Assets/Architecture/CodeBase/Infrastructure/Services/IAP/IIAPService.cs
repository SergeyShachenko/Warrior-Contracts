using System;
using System.Collections.Generic;

namespace CodeBase.Infrastructure.Services.IAP
{
  public interface IIAPService : IService
  {
    event Action Initizlized;
    bool IsInitialized { get; }
    void Init();
    List<ProductDescription> GetProductDescriptions();
    void StartPurchase(string productID);
  }
}
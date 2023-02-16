using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Data;
using CodeBase.Infrastructure.Services.PersistentProgress;
using UnityEngine.Purchasing;

namespace CodeBase.Infrastructure.Services.IAP
{
  public class IAPService : IIAPService
  {
    public event Action Initizlized;
    
    public bool IsInitialized => _iapProvider.IsInitialized;

    private readonly IAPProvider _iapProvider;
    private readonly IPersistentProgressService _progressService;

    public IAPService(IAPProvider iapProvider, IPersistentProgressService progressService)
    {
      _iapProvider = iapProvider;
      _progressService = progressService;
    }

    public void Init()
    {
      _iapProvider.Init(this);
      _iapProvider.Initialized += () => Initizlized?.Invoke();
    }


    public List<ProductDescription> GetProductDescriptions() =>
      CreateProductDescriptions().ToList();

    public void StartPurchase(string productID) => 
      _iapProvider.StartPurchase(productID);

    public PurchaseProcessingResult ProcessPurchase(Product product)
    {
      ProductConfig productConfig = _iapProvider.Configs[product.definition.id];

      switch (productConfig.ItemType)
      {
        case ItemType.Gold:
        {
          _progressService.Progress.World.Loot.Add(productConfig.Amount);
          _progressService.Progress.Purchase.AddPurchase(product.definition.id);
        }
          break;
      }

      return PurchaseProcessingResult.Complete;
    }

    private IEnumerable<ProductDescription> CreateProductDescriptions()
    {
      PurchaseData purchaseData = _progressService.Progress.Purchase;

      foreach (var productID in _iapProvider.Products.Keys)
      {
        ProductConfig config = _iapProvider.Configs[productID];
        Product product = _iapProvider.Products[productID];

        BoughtProduct boughtProduct = purchaseData.BoughtProducts.Find(x => x.ID == productID);
        
        if (IsBoughtOut(boughtProduct, config)) continue;


        yield return new ProductDescription
        {
          ID = productID,
          Config = config,
          Product = product,
          AvailablePurchasesLeft = boughtProduct != null
            ? config.MaxPurchaseCount - boughtProduct.Count
            : config.MaxPurchaseCount
        };
      }
    }

    private bool IsBoughtOut(BoughtProduct boughtProduct, ProductConfig config) => 
      boughtProduct != null && boughtProduct.Count >= config.MaxPurchaseCount;
  }
}
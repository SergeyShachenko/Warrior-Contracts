using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Purchasing;
using WC.Runtime.Data;
using WC.Runtime.Infrastructure.AssetManagement;

namespace WC.Runtime.Infrastructure.Services
{
  public class IAPProvider : IStoreListener
  {
    public event Action Initialized;
    
    public bool IsInitialized => _controller != null && _extensions != null;
    public Dictionary<string, ProductConfig> Configs { get; private set; }
    public Dictionary<string, Product> Products { get; private set; }

    private IStoreController _controller;
    private IExtensionProvider _extensions;
    private IIAPService _iapService;


    public void Init(IIAPService iapService)
    {
      _iapService = iapService;
      Configs = new Dictionary<string, ProductConfig>();
      Products = new Dictionary<string, Product>();

      LoadConfigs();
      
      var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

      foreach (ProductConfig config in Configs.Values) 
        builder.AddProduct(config.ID, config.ProductType);

      UnityPurchasing.Initialize(this, builder);
    }


    public void StartPurchase(string productID) => 
      _controller.InitiatePurchase(productID);

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
      _controller = controller;
      _extensions = extensions;

      foreach (Product product in _controller.products.all) 
        Products.Add(product.definition.id, product);

      Initialized?.Invoke();
      
      Debug.Log("UnityPurchasing: Initialization <color=Green>success</color>");
    }

    public void OnInitializeFailed(InitializationFailureReason error) => 
      Debug.LogError($"UnityPurchasing: Initialization <color=Red>failed</color> - {error}");

    public void OnInitializeFailed(InitializationFailureReason error, string message) => 
      Debug.LogError($"UnityPurchasing: Initialization <color=Red>failed</color> - {error}. {message}");

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
      Product product = purchaseEvent.purchasedProduct;
      
      Debug.Log($"UnityPurchasing: Product <b>{product.definition.id}</b> purchase <color=Green>success</color>. " +
                            $"Transaction ID - <color=Yellow>{product.transactionID}</color>");

      return _iapService.ProcessPurchase(purchaseEvent.purchasedProduct);
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason) => 
      Debug.LogError($"UnityPurchasing: Product <b>{product.definition.id}</b> purchase <color=Red>failed</color> - {failureReason}. " +
                                 $"Transaction ID - <color=Yellow>{product.transactionID}</color>");

    private void LoadConfigs()
    {
      Configs = Resources
        .Load<TextAsset>(AssetDirectory.Config.Root + AssetName.Config.IAP).text
        .ToDeserialized<ProductConfigWrapper>().Configs
        .ToDictionary(x => x.ID, x => x);
    }
  }
}
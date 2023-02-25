using System;
using System.Collections.Generic;

namespace WC.Runtime.Data.IAP
{
  [Serializable]
  public class PurchaseData
  {
    public event Action Changed;
    
    public List<BoughtProductData> BoughtProducts = new();
    
    
    public void AddPurchase(string productID)
    {
      BoughtProductData boughtProduct = BoughtProducts.Find(x => x.ID == productID);

      if (boughtProduct != null)
        boughtProduct.Count++;
      else
        BoughtProducts.Add(new BoughtProductData { ID = productID, Count = 1 });
      
      Changed?.Invoke();
    }

    public PurchaseData Copy()
    {
      var copy = new PurchaseData();

      foreach (BoughtProductData product in BoughtProducts) 
        copy.BoughtProducts.Add(product);

      return copy;
    }
  }
}
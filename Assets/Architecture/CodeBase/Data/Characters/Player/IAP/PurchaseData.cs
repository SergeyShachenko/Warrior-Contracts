using System;
using System.Collections.Generic;

namespace CodeBase.Data
{
  [Serializable]
  public class PurchaseData
  {
    public event Action Changed;
    
    public List<BoughtProduct> BoughtProducts = new();
    
    
    public void AddPurchase(string productID)
    {
      BoughtProduct boughtProduct = BoughtProducts.Find(x => x.ID == productID);

      if (boughtProduct != null)
        boughtProduct.Count++;
      else
        BoughtProducts.Add(new BoughtProduct { ID = productID, Count = 1 });
      
      Changed?.Invoke();
    }
  }
}
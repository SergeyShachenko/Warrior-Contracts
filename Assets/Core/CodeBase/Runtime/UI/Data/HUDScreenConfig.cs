using System;
using UnityEngine.AddressableAssets;
using WC.Runtime.UI.Elements;

namespace WC.Runtime.UI.Data
{
  [Serializable]
  public class HUDScreenConfig
  {
    public HUDScreenID ID;
    public AssetReferenceGameObject PrefabRef;
  }
}
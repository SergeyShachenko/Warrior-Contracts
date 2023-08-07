using System;
using UnityEngine.AddressableAssets;
using WC.Runtime.UI.Elements;

namespace WC.Runtime.UI.Data
{
  [Serializable]
  public class HUDWindowConfig
  {
    public HUDWindowID ID;
    public AssetReferenceGameObject PrefabRef;
  }
}
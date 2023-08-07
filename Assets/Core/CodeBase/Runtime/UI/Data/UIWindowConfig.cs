using System;
using UnityEngine.AddressableAssets;
using WC.Runtime.UI.Elements;

namespace WC.Runtime.UI.Data
{
  [Serializable]
  public class UIWindowConfig
  {
    public UIWindowID ID;
    public AssetReferenceGameObject PrefabRef;
  }
}
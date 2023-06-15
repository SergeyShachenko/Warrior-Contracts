using System;
using UnityEngine.AddressableAssets;
using WC.Runtime.UI;

namespace WC.Runtime.Data.UI
{
  [Serializable]
  public class UIWindowConfig
  {
    public UIWindowID ID;
    public AssetReferenceGameObject PrefabRef;
  }
}
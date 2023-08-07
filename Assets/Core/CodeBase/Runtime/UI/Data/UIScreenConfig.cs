using System;
using UnityEngine.AddressableAssets;
using WC.Runtime.UI.Elements;

namespace WC.Runtime.UI.Data
{
  [Serializable]
  public class UIScreenConfig
  {
    public UIScreenID ID;
    public AssetReferenceGameObject PrefabRef;
  }
}
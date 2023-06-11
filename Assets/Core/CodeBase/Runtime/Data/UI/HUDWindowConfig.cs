using System;
using UnityEngine.AddressableAssets;
using WC.Runtime.UI.Windows;

namespace WC.Runtime.Data.UI
{
  [Serializable]
  public class HUDWindowConfig
  {
    public HUDWindowID ID;
    public AssetReferenceGameObject PrefabRef;
  }
}
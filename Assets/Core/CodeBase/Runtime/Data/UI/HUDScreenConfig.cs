using System;
using UnityEngine.AddressableAssets;
using WC.Runtime.UI.Screens;

namespace WC.Runtime.Data.UI
{
  [Serializable]
  public class HUDScreenConfig
  {
    public HUDScreenID ID;
    public AssetReferenceGameObject PrefabRef;
  }
}
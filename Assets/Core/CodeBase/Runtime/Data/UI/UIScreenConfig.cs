using System;
using UnityEngine.AddressableAssets;
using WC.Runtime.UI.Screens;

namespace WC.Runtime.Data.UI
{
  [Serializable]
  public class UIScreenConfig
  {
    public UIScreenID ID;
    public AssetReferenceGameObject PrefabRef;
  }
}
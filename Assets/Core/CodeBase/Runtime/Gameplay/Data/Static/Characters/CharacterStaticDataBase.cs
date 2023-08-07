using UnityEngine;
using UnityEngine.AddressableAssets;
using WC.Runtime.Gameplay.Data;

namespace WC.Runtime.Gameplay.Data
{
  public abstract class CharacterStaticDataBase : ScriptableObject
  {
    [field: SerializeField, Header("")] public AssetReferenceGameObject PrefabRef { get; private set; }
  }
}
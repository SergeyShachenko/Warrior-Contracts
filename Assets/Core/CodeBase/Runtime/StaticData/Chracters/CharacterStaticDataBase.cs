using UnityEngine;
using UnityEngine.AddressableAssets;
using WC.Runtime.Data.Characters;

namespace WC.Runtime.StaticData
{
  public abstract class CharacterStaticDataBase : ScriptableObject
  {
    [field: SerializeField, Header("")] public AssetReferenceGameObject PrefabRef { get; private set; }
  }
}
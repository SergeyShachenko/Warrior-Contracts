using UnityEngine;

namespace WC.Runtime.UI.Data
{
  [CreateAssetMenu(menuName = "Dev/StaticData/UI/UI_Windows", fileName = "UI_Windows")]
  public class UIWindowsStaticData : ScriptableObject
  {
    [field: SerializeField] public UIWindowConfig[] Windows { get; private set; }
  }
}
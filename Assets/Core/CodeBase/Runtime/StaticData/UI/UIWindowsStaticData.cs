using UnityEngine;
using WC.Runtime.Data.UI;

namespace WC.Runtime.StaticData
{
  [CreateAssetMenu(menuName = "Dev/StaticData/UI/UI_Windows", fileName = "UI_Windows")]
  public class UIWindowsStaticData : ScriptableObject
  {
    [field: SerializeField] public UIWindowConfig[] Windows { get; private set; }
  }
}
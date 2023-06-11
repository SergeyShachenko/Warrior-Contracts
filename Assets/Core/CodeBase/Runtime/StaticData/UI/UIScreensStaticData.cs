using UnityEngine;
using WC.Runtime.Data.UI;

namespace WC.Runtime.StaticData
{
  [CreateAssetMenu(menuName = "Dev/StaticData/UI/UI_Screens", fileName = "UI_Screens")]
  public class UIScreensStaticData : ScriptableObject
  {
    [field: SerializeField] public UIScreenConfig[] Screens { get; private set; }
  }
}
using UnityEngine;

namespace WC.Runtime.UI.Data
{
  [CreateAssetMenu(menuName = "Dev/StaticData/UI/UI_Screens", fileName = "UI_Screens")]
  public class UIScreensStaticData : ScriptableObject
  {
    [field: SerializeField] public UIScreenConfig[] Screens { get; private set; }
  }
}
using UnityEngine;

namespace WC.Runtime.UI.Data
{
  [CreateAssetMenu(menuName = "Dev/StaticData/UI/HUD_Screens", fileName = "HUD_Screens")]
  public class HUDScreensStaticData : ScriptableObject
  {
    [field: SerializeField] public HUDScreenConfig[] Screens { get; private set; }
  }
}
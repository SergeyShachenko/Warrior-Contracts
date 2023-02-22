using System.Collections.Generic;
using UnityEngine;
using WC.Runtime.Data;

namespace WC.Runtime.StaticData
{
  [CreateAssetMenu(menuName = "Dev/StaticData/Configs/Windows", fileName = "Configs_UI_Windows")]
  public class WindowStaticData : ScriptableObject
  {
    public List<WindowConfig> Configs;
  }
}
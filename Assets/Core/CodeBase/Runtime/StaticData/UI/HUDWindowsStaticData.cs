﻿using UnityEngine;
using WC.Runtime.Data.UI;

namespace WC.Runtime.StaticData
{
  [CreateAssetMenu(menuName = "Dev/StaticData/UI/HUD_Windows", fileName = "HUD_Windows")]
  public class HUDWindowsStaticData : ScriptableObject
  {
    [field: SerializeField] public HUDWindowConfig[] Windows { get; private set; }
  }
}
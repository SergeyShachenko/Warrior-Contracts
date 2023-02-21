﻿using System.Collections.Generic;
using CodeBase.Data;
using UnityEngine;

namespace CodeBase.Data.StaticData
{
  [CreateAssetMenu(menuName = "Dev/StaticData/Configs/Windows", fileName = "Configs_UI_Windows")]
  public class WindowStaticData : ScriptableObject
  {
    public List<WindowConfigStaticData> Configs;
  }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace WC.Runtime.Gameplay.Data
{
  [Serializable]
  public class KillData
  {
    public List<string> ClearedSpawners = new();


    public KillData GetCopy() => new()
    {
      ClearedSpawners = ClearedSpawners.ToList()
    };
  }
}
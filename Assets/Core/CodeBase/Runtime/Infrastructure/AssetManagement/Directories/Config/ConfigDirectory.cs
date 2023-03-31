using UnityEngine;

namespace WC.Runtime.Infrastructure.AssetManagement
{
  public class ConfigDirectory
  {
    public string Root => Application.streamingAssetsPath + "/Configs/";
  }
}
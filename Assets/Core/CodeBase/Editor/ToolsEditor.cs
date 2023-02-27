using UnityEditor;
using UnityEngine;

namespace WC.Editor
{
  public static class ToolsEditor
  {
    [MenuItem("Tools/Clear Prefs")]
    public static void ClearPrefs()
    {
      PlayerPrefs.DeleteAll();
      PlayerPrefs.Save();
    }
  }
}

using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using WC.Runtime.Logic.Characters;
using WC.Runtime.Logic.Tools;
using WC.Runtime.Data.Characters;
using WC.Runtime.StaticData;

namespace WC.Editor
{
  [CustomEditor(typeof(LevelStaticData))]
  public class LevelStaticDataEditor : UnityEditor.Editor
  {
    private const string PlayerSpawnPointTag = "PlayerSpawnPoint";
    
    
    public override void OnInspectorGUI()
    {
      base.OnInspectorGUI();

      var levelData = (LevelStaticData)target;

      if (GUILayout.Button("Collect"))
      {
        levelData.EnemySpawners =
          FindObjectsOfType<SpawnMarker>()
            .Select(x => new EnemySpawnerData(x.GetComponent<UniqueID>().ID, x.WarriorType, x.transform.position))
            .ToList();

        levelData.LevelKey = SceneManager.GetActiveScene().name;
        levelData.InitPlayerPos = GameObject.FindWithTag(PlayerSpawnPointTag).transform.position;
      }
      
      EditorUtility.SetDirty(target);
    }
  }
}
using System.Linq;
using CodeBase.Data;
using CodeBase.Data.StaticData;
using CodeBase.Logic;
using CodeBase.Logic.Characters;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Editor
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
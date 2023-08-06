using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using WC.Runtime.Logic.Characters;
using WC.Runtime.Logic.Tools;
using WC.Runtime.Data.Characters;
using WC.Runtime.Infrastructure.AssetManagement;
using WC.Runtime.StaticData;

namespace WC.Editor
{
  [CustomEditor(typeof(LevelStaticData))]
  public class LevelStaticDataEditor : UnityEditor.Editor
  {
    public override void OnInspectorGUI()
    {
      base.OnInspectorGUI();

      var levelData = (LevelStaticData)target;

      if (GUILayout.Button("Collect active scene data"))
      {
        levelData.Name = SceneManager.GetActiveScene().name;
        
        Transform playerSpawn = GameObject.FindWithTag(AssetTag.PlayerSpawnMarker).transform;
        levelData.PlayerSpawner.Position = playerSpawn.position;
        levelData.PlayerSpawner.Rotation = playerSpawn.rotation;
        
        levelData.EnemySpawners =
          FindObjectsOfType<EnemySpawnMarker>()
            .Select(x => new EnemySpawnData(x.GetComponent<UniqueID>().ID, x.WarriorType, x.transform.position, x.transform.rotation))
            .ToList();
      }
      
      EditorUtility.SetDirty(target);
    }
  }
}
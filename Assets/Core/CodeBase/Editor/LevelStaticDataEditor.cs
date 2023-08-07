using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using WC.Runtime.Gameplay.Data;
using WC.Runtime.Gameplay.Logic;
using WC.Runtime.Gameplay.Tools;
using WC.Runtime.Infrastructure.AssetManagement;

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
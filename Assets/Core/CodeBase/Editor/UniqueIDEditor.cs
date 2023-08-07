using System;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using WC.Runtime.Gameplay.Tools;

namespace WC.Editor
{
  [CustomEditor(typeof(UniqueID))]
  public class UniqueIDEditor : UnityEditor.Editor
  {
    private void OnEnable()
    {
      var uniqueID = (UniqueID) target;

      if (string.IsNullOrEmpty(uniqueID.ID))
        GenerateID(uniqueID);
      else
      {
        UniqueID[] uniqueIDs = FindObjectsOfType<UniqueID>();
        
        if (uniqueIDs.Any(x => x.ID == uniqueID.ID && x != uniqueID))
          GenerateID(uniqueID);
      }
    }

    private void GenerateID(UniqueID uniqueID)
    {
      uniqueID.ID = $"{uniqueID.gameObject.scene.name}-{Guid.NewGuid().ToString()}";

      if (Application.isPlaying == false)
      {
        EditorUtility.SetDirty(uniqueID);
        EditorSceneManager.MarkSceneDirty(uniqueID.gameObject.scene); 
      }
    }
  }
}
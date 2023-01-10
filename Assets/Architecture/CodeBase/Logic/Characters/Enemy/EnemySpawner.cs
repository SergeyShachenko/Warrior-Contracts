using CodeBase.Data;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Logic.Characters.Enemy
{
  public class EnemySpawner : MonoBehaviour, 
    ISaverProgress
  {
    public string ID => _uniqueID.ID;
    [field: SerializeField] public bool IsCleared { get; set; }

    [SerializeField] private WarriorType _warriorType;
    
    [Header("Links")]
    [SerializeField] private UniqueID _uniqueID;


    private void Spawn()
    {
      
    }

    public void LoadProgress(PlayerProgress progress)
    {
      if (progress.KillData.ClearedSpawners.Contains(ID)) 
        IsCleared = true;
      else
        Spawn();
    }

    public void SaveProgress(PlayerProgress progress)
    {
      if (IsCleared) 
        progress.KillData.ClearedSpawners.Add(ID);
    }
  }
}
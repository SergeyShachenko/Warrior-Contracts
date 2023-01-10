using CodeBase.Data;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Logic.Characters.Enemy
{
  public class EnemySpawner : MonoBehaviour, 
    ISaveProgress
  {
    public string ID => _uniqueID.ID;
    [field: SerializeField] public bool IsCleared { get; set; }

    [SerializeField] private EnemyTypeID _enemyTypeID;
    
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
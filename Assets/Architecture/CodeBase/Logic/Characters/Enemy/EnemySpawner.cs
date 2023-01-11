using CodeBase.Data;
using CodeBase.Infrastructure.Factories;
using CodeBase.Infrastructure.Services;
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

    private IGameFactory _factory;
    private EnemyDeath _enemyDeath;


    private void Awake()
    {
      _factory = AllServices.Container.Single<IGameFactory>();
    }


    private void Spawn()
    {
      GameObject enemyWarrior = _factory.CreateEnemyWarrior(_warriorType, transform);
      _enemyDeath = enemyWarrior.GetComponent<EnemyDeath>();
      _enemyDeath.Happened += OnEnemyDead;
    }

    private void OnEnemyDead()
    {
      if (_enemyDeath != null)
        _enemyDeath.Happened -= OnEnemyDead;
      
      IsCleared = true;
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
using CodeBase.Data;
using CodeBase.Infrastructure.Factories;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Logic.Characters
{
  public class SpawnPoint : MonoBehaviour, 
    ISaverProgress
  {
    public WarriorType WarriorType;
    
    [HideInInspector] public string ID;

    private IGameFactory _gameFactory;
    private EnemyDeath _enemyDeath;
    private bool _isCleared;

    public void Construct(IGameFactory gameFactory)
    {
      _gameFactory = gameFactory;
    }
    
    
    private void Spawn()
    {
      GameObject enemyWarrior = _gameFactory.CreateEnemyWarrior(WarriorType, transform);
      _enemyDeath = enemyWarrior.GetComponent<EnemyDeath>();
      _enemyDeath.Happened += OnEnemyDead;
    }

    private void OnEnemyDead()
    {
      if (_enemyDeath != null)
        _enemyDeath.Happened -= OnEnemyDead;
      
      _isCleared = true;
    }

    public void LoadProgress(PlayerProgressData progressData)
    {
      if (progressData.Kill.ClearedSpawners.Contains(ID)) 
        _isCleared = true;
      else
        Spawn();
    }

    public void SaveProgress(PlayerProgressData progressData)
    {
      if (_isCleared) 
        progressData.Kill.ClearedSpawners.Add(ID);
    }
  }
}
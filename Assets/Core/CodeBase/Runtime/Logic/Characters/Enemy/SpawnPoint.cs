﻿using UnityEngine;
using WC.Runtime.Data.Characters;
using WC.Runtime.Infrastructure.Services;

namespace WC.Runtime.Logic.Characters
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
    
    
    private async void Spawn()
    {
      GameObject enemyWarrior = await _gameFactory.CreateEnemyWarrior(WarriorType, transform);
      
      
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
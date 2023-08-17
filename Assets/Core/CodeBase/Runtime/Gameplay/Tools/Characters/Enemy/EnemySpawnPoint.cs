﻿using UnityEngine;
using UnityEngine.Serialization;
using WC.Runtime.Gameplay.Data;
using WC.Runtime.Gameplay.Services;
using WC.Runtime.Infrastructure.Services;
using Zenject;

namespace WC.Runtime.Gameplay.Logic
{
  public class EnemySpawnPoint : MonoBehaviour,
    ISaverProgress
  {
    [FormerlySerializedAs("_warriorType")] [SerializeField] private EnemyID _type;
    
    private ICharacterFactory _characterFactory;
    private PlayerProgressData _progress;
    private Enemy _enemy;
    private EnemyDeath _enemyDeath;

    private string _id;
    private bool _isCleared;

    [Inject]
    private void Construct(ICharacterFactory characterFactory) => 
      _characterFactory = characterFactory;

    
    public void Init(EnemyID type, string id)
    {
      _type = type;
      _id = id;
    }

    private void PostInit()
    {
      _enemyDeath = (EnemyDeath)_enemy.Death;
      _enemyDeath.Happened += OnEnemyDead;
    }

    private async void Spawn()
    {
      _enemy = await _characterFactory.CreateEnemy(_type, transform);
      _enemy.Initialized += PostInit;
    }

    private void OnEnemyDead()
    {
      if (_enemyDeath != null)
        _enemyDeath.Happened -= OnEnemyDead;
      
      _isCleared = true;
    }

    void ILoaderProgress.LoadProgress(PlayerProgressData progressData)
    {
      _progress = progressData;
      
      if (_progress.Kill.ClearedSpawners.Contains(_id)) 
        _isCleared = true;
      else
        Spawn();
    }

    void ISaverProgress.SaveProgress(PlayerProgressData progressData)
    {
      if (_isCleared) 
        progressData.Kill.ClearedSpawners.Add(_id);
    }
  }
}
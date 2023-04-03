using UnityEngine;
using WC.Runtime.Data.Characters;
using WC.Runtime.Gameplay.Services;
using WC.Runtime.Infrastructure.Services;
using Zenject;

namespace WC.Runtime.Logic.Characters
{
  public class EnemySpawnPoint : MonoBehaviour,
    ISaverProgress
  {
    [SerializeField] private WarriorID _warriorType;
    
    private ICharacterFactory _characterFactory;
    private PlayerProgressData _progress;
    private EnemyDeath _enemyDeath;
    
    private string _id;
    private bool _isCleared;

    [Inject]
    private void Construct(ICharacterFactory characterFactory) => 
      _characterFactory = characterFactory;

    
    public void Init(WarriorID warriorType, string id)
    {
      _warriorType = warriorType;
      _id = id;
    }


    private async void Spawn()
    {
      Enemy enemy = await _characterFactory.CreateEnemy(_warriorType, transform);
      
      _enemyDeath = (EnemyDeath)enemy.Death;
      _enemyDeath.Happened += OnEnemyDead;
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
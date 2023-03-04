using UnityEngine;
using WC.Runtime.Data.Characters;
using WC.Runtime.Gameplay.Services;
using WC.Runtime.Infrastructure.Services;

namespace WC.Runtime.Logic.Characters
{
  public class EnemySpawnPoint : MonoBehaviour,
    ISaverProgress
  {
    public WarriorType WarriorType;
    
    [HideInInspector] public string ID;

    private ICharacterFactory _characterFactory;
    private PlayerProgressData _progress;
    private EnemyDeath _enemyDeath;
    private bool _isCleared;

    public void Construct(ICharacterFactory characterFactory) => 
      _characterFactory = characterFactory;

    private void Init()
    {
      if (_progress.Kill.ClearedSpawners.Contains(ID)) 
        _isCleared = true;
      else
        Spawn();
    }


    private async void Spawn()
    {
      GameObject enemyWarrior = await _characterFactory.CreateEnemy(WarriorType, transform);
      
      _enemyDeath = (EnemyDeath)enemyWarrior.GetComponent<Enemy>().Death;
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
      Init();
    }

    void ISaverProgress.SaveProgress(PlayerProgressData progressData)
    {
      if (_isCleared) 
        progressData.Kill.ClearedSpawners.Add(ID);
    }
  }
}
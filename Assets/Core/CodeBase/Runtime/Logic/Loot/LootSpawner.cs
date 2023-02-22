using UnityEngine;
using WC.Runtime.Data;
using WC.Runtime.Infrastructure.Services;
using WC.Runtime.Logic.Characters;

namespace WC.Runtime.Logic.Loot
{
  public class LootSpawner : MonoBehaviour
  {
    [Header("Links")]
    [SerializeField] private EnemyDeath _enemyDeath;
    
    private IGameFactory _gameFactory;
    private IRandomService _randomService;
    private int _minLootExp, _maxLootExp;

    public void Construct(IGameFactory gameFactory, IRandomService randomService)
    {
      _gameFactory = gameFactory;
      _randomService = randomService;
    }


    private void Start()
    {
      _enemyDeath.Happened += OnEnemyDead;
    }


    public void SetLootExp(int min, int max)
    {
      _minLootExp = min;
      _maxLootExp = max;
    }

    private void OnEnemyDead()
    {
      SpawnLoot();
    }

    private async void SpawnLoot()
    {
      LootPiece loot = await _gameFactory.CreateLoot();
      
      
      loot.transform.position = transform.position;
      LootData lootExp = new(value: _randomService.Next(_minLootExp, _maxLootExp));
      loot.Init(lootExp);
      
      // TODO Сделать механику сохранения лута
      // _gameFactory.Register(loot);
      // loot.Picked += () => _gameFactory.Unregister(loot);
    }
  }
}
using UnityEngine;
using WC.Runtime.Data;
using WC.Runtime.Gameplay.Services;
using WC.Runtime.Infrastructure.Services;
using WC.Runtime.Logic.Characters;

namespace WC.Runtime.Logic.Loot
{
  public class LootSpawner : MonoBehaviour
  {
    [Header("Links")]
    [SerializeField] private Enemy _enemy;
    
    private ILootFactory _lootFactory;
    private IRandomService _randomService;
    
    private int _minLootGold, _maxLootGold;

    public void Construct(ILootFactory lootFactory, IRandomService randomService)
    {
      _lootFactory = lootFactory;
      _randomService = randomService;
      
      Init();
    }

    private void Init() => 
      _enemy.Death.Happened += OnEnemyDead;


    public void SetLootExp(int min, int max)
    {
      _minLootGold = min;
      _maxLootGold = max;
    }

    private void OnEnemyDead() => 
      DropLoot();

    private async void DropLoot()
    {
      LootPiece loot = await _lootFactory.CreateGold();
      
      loot.transform.position = transform.position;
      LootData lootExp = new(value: _randomService.Next(_minLootGold, _maxLootGold));
      loot.Init(lootExp);
      
      // TODO Сделать механику сохранения лута
      // _gameFactory.Register(loot);
      // loot.Picked += () => _gameFactory.Unregister(loot);
    }
  }
}
using UnityEngine;
using WC.Runtime.Data;
using WC.Runtime.Gameplay.Services;
using WC.Runtime.Infrastructure.Services;
using WC.Runtime.Logic.Characters;
using Zenject;

namespace WC.Runtime.Logic.Loot
{
  public class LootSpawner : MonoBehaviour
  {
    [Header("Links")]
    [SerializeField] private Enemy _enemy;
    
    private ILootFactory _lootFactory;
    private IRandomService _randomService;
    
    private int _minGold, _maxGold;

    [Inject]
    private void Construct(ILootFactory lootFactory, IRandomService randomService)
    {
      _lootFactory = lootFactory;
      _randomService = randomService;
    }


    public void Init(int minGold, int maxGold)
    {
      _minGold = minGold;
      _maxGold = maxGold;

      _enemy.Initialized += PostInit;
    }

    private void PostInit() => _enemy.Death.Happened += OnEnemyDead;


    private void OnEnemyDead() => DropLoot();

    private async void DropLoot()
    {
      LootPiece loot = await _lootFactory.CreateGold();
      
      loot.transform.position = transform.position;
      LootData lootExp = new(value: _randomService.Next(_minGold, _maxGold));
      loot.Init(lootExp);
      
      // TODO Сделать механику сохранения лута
      // _gameFactory.Register(loot);
      // loot.Picked += () => _gameFactory.Unregister(loot);
    }
  }
}
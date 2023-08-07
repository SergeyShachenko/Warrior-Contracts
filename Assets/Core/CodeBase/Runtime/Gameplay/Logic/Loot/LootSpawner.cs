using UnityEngine;
using WC.Runtime.Gameplay.Data;
using WC.Runtime.Gameplay.Logic;
using WC.Runtime.Gameplay.Services;
using WC.Runtime.Infrastructure.Services;
using Zenject;

namespace WC.Runtime.Gameplay.Logic
{
  public class LootSpawner : MonoBehaviour
  {
    [Header("Links")]
    [SerializeField] private Enemy _enemy;
    
    private ILootFactory _lootFactory;
    private IRandomService _randomService;
    
    private int _minMoney, _maxMoney;

    [Inject]
    private void Construct(ILootFactory lootFactory, IRandomService randomService)
    {
      _lootFactory = lootFactory;
      _randomService = randomService;
    }


    public void SetData(int loot)
    {
      _minMoney = 0;
      _maxMoney = loot;

      _enemy.Initialized += PostInit;
    }

    private void PostInit() => _enemy.Death.Happened += OnEnemyDead;


    private void OnEnemyDead() => DropLoot();

    private async void DropLoot()
    {
      LootPiece loot = await _lootFactory.CreateGold();
      
      loot.transform.position = transform.position;
      LootData lootExp = new(value: _randomService.Next(_minMoney, _maxMoney));
      loot.Init(lootExp);
      
      // TODO Сделать механику сохранения лута
      // _gameFactory.Register(loot);
      // loot.Picked += () => _gameFactory.Unregister(loot);
    }
  }
}
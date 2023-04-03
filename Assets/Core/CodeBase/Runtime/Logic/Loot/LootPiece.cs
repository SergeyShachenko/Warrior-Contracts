using UnityEngine;
using WC.Runtime.Data;
using WC.Runtime.Infrastructure.Services;
using Zenject;

namespace WC.Runtime.Logic.Loot
{
  public class LootPiece : MonoBehaviour
  {
    private LootData _lootData;
    private bool _picked;
    private IPersistentProgressService _progress;

    [Inject]
    private void Construct(IPersistentProgressService progress) => _progress = progress;

    
    public void Init(LootData lootData) => _lootData = lootData;
    private void OnTriggerEnter(Collider other) => Pickup();


    private void Pickup()
    {
      if (_picked) return;
      
      
      _picked = true;
      _progress.Player.World.Loot.Collect(_lootData);
      
      Destroy(gameObject);
    }
  }
}
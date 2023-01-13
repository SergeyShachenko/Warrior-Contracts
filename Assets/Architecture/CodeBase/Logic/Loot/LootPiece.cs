using CodeBase.Data;
using UnityEngine;

namespace CodeBase.Logic.Loot
{
  public class LootPiece : MonoBehaviour
  {
    private WorldData _worldData;
    private LootData _lootData;
    private bool _picked;

    public void Construct(WorldData worldData)
    {
      _worldData = worldData;
    }
    
    public void Init(LootData lootData)
    {
      _lootData = lootData;
    }


    private void OnTriggerEnter(Collider other)
    {
      Pickup();
    }

    
    private void Pickup()
    {
      if (_picked) return;
      
      
      _picked = true;
      _worldData.AllLoot.Collect(_lootData);
      Destroy(gameObject);
    }
  }
}
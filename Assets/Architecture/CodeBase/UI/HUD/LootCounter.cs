using CodeBase.Data;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.HUD
{
  public class LootCounter : MonoBehaviour
  {
    public TextMeshProUGUI Counter;
    private WorldData _worldData;

    public void Construct(WorldData worldData)
    {
      _worldData = worldData;
      _worldData.AllLoot.Changed += UpdateCounter;
    }


    private void Start() => 
      UpdateCounter();


    private void UpdateCounter() => 
      Counter.text = $"{_worldData.AllLoot.Collected}";
  }
}
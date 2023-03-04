using TMPro;
using UnityEngine;

namespace WC.Runtime.UI
{
  public class LootCounter : MonoBehaviour
  {
    [SerializeField] private TextMeshProUGUI _counter;
    

    public void Set(float count) => 
      _counter.text = $"{count}";
  }
}
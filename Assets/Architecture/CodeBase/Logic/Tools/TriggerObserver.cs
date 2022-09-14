using System;
using UnityEngine;

namespace CodeBase.Logic.Tools
{
  [RequireComponent(typeof(Collider))]
  public class TriggerObserver : MonoBehaviour
  {
    public event Action<Collider> TriggerEnter, TriggerExit; 
    
    
    private void OnTriggerEnter(Collider other) => 
      TriggerEnter?.Invoke(other);

    private void OnTriggerExit(Collider other) => 
      TriggerExit?.Invoke(other);
  }
}
using System;
using UnityEngine;

namespace WC.Runtime.Logic.Tools
{
  [RequireComponent(typeof(Collider))]
  public class TriggerObserver : MonoBehaviour
  {
    public event Action<Collider> TriggerEnter, TriggerExit;

    public float Radius 
    { 
      get => _collider.radius;
      set => _collider.radius = value;
    }

    [SerializeField] private SphereCollider _collider;
    
    
    private void OnTriggerEnter(Collider other) => 
      TriggerEnter?.Invoke(other);

    private void OnTriggerExit(Collider other) => 
      TriggerExit?.Invoke(other);
  }
}
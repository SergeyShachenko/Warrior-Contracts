using System;
using UnityEngine;
using WC.Runtime.Extensions;

namespace WC.Runtime.Gameplay.Tools
{
  [RequireComponent(typeof(Collider))]
  public class ZoneTrigger : MonoBehaviour
  {
    public event Action<Collider> TriggerEnter, TriggerExit;

    public float Radius
    {
      get => _collider.radius;
      set
      {
        if (ID == ZoneTriggerID.CloseCombat)
        {
          Vector3 newCenter = _collider.center.SetZ(value);
          _collider.center = newCenter;
        }

        _collider.radius = value;
      }
    }

    [field: SerializeField] public ZoneTriggerID ID { get; private set; }
    [SerializeField] private SphereCollider _collider;
    
    
    private void OnTriggerEnter(Collider target) => 
      TriggerEnter?.Invoke(target);

    private void OnTriggerExit(Collider target) => 
      TriggerExit?.Invoke(target);
  }
}
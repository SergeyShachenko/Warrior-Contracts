using UnityEngine;
using WC.Runtime.Logic.Tools;

namespace WC.Runtime.Logic.Characters
{
  public class CheckAttackRange : MonoBehaviour
  {
    [Header("Links")]
    [SerializeField] private Enemy _enemy;
    [SerializeField] private TriggerObserver _triggerObserver;


    private void Start()
    {
      _triggerObserver.TriggerEnter += OnObserverTriggerEnter;
      _triggerObserver.TriggerExit += OnObserverTriggerExit;
    }

    
    private void OnObserverTriggerEnter(Collider obj) => 
      _enemy.Attack.IsActive = true;

    private void OnObserverTriggerExit(Collider obj) => 
      _enemy.Attack.IsActive = false;
  }
}
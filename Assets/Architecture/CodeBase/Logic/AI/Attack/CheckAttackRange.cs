using CodeBase.Logic.Tools;
using UnityEngine;

namespace CodeBase.Logic.AI
{
  [RequireComponent(typeof(AttackAI))]
  public class CheckAttackRange : MonoBehaviour
  {
    [Header("Links")]
    [SerializeField] private AttackAI _attackAI;
    [SerializeField] private TriggerObserver _triggerObserver;


    private void Start()
    {
      _triggerObserver.TriggerEnter += OnObserverTriggerEnter;
      _triggerObserver.TriggerExit += OnObserverTriggerExit;
    }

    
    private void OnObserverTriggerEnter(Collider obj) => 
      _attackAI.IsActive = true;

    private void OnObserverTriggerExit(Collider obj) => 
      _attackAI.IsActive = false;
  }
}
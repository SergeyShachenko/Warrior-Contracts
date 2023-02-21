using CodeBase.Logic.Tools;
using UnityEngine;

namespace CodeBase.Logic.Characters
{
  [RequireComponent(typeof(EnemyAttack))]
  public class CheckAttackRange : MonoBehaviour
  {
    [Header("Links")]
    [SerializeField] private EnemyAttack _enemyAttack;
    [SerializeField] private TriggerObserver _triggerObserver;


    private void Start()
    {
      _triggerObserver.TriggerEnter += OnObserverTriggerEnter;
      _triggerObserver.TriggerExit += OnObserverTriggerExit;
    }

    
    private void OnObserverTriggerEnter(Collider obj) => 
      _enemyAttack.IsActive = true;

    private void OnObserverTriggerExit(Collider obj) => 
      _enemyAttack.IsActive = false;
  }
}
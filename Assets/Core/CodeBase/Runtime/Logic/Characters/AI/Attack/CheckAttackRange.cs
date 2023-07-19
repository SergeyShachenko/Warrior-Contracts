using UnityEngine;
using WC.Runtime.Logic.Tools;

namespace WC.Runtime.Logic.Characters
{
  public class CheckAttackRange : MonoBehaviour
  {
    [Header("Links")]
    [SerializeField] private CharacterBase _character;
    [SerializeField] private TriggerObserver _triggerObserver;


    private void Start()
    {
      _triggerObserver.Radius = _character.Attack.AttackDistance;
      
      _triggerObserver.TriggerEnter += OnObserverTriggerEnter;
      _triggerObserver.TriggerExit += OnObserverTriggerExit;
    }

    private void OnDestroy()
    {
      _triggerObserver.TriggerEnter -= OnObserverTriggerEnter;
      _triggerObserver.TriggerExit -= OnObserverTriggerExit;
    }


    private void OnObserverTriggerEnter(Collider obj) => 
      _character.Attack.IsActive = true;

    private void OnObserverTriggerExit(Collider obj) => 
      _character.Attack.IsActive = false;
  }
}
using UnityEngine;
using WC.Runtime.Gameplay.Tools;

namespace WC.Runtime.Gameplay.Logic
{
  public class CheckAttackRange : MonoBehaviour
  {
    [Header("Links")]
    [SerializeField] private CharacterBase _character;
    [SerializeField] private TriggerObserver _triggerObserver;


    private void Start()
    {
      _character.Initialized += OnInitCharacter;
      _triggerObserver.TriggerEnter += OnObserverTriggerEnter;
      _triggerObserver.TriggerExit += OnObserverTriggerExit;
    }

    private void OnDestroy()
    {
      _character.Initialized -= OnInitCharacter;
      _triggerObserver.TriggerEnter -= OnObserverTriggerEnter;
      _triggerObserver.TriggerExit -= OnObserverTriggerExit;
    }


    private void OnInitCharacter() => _triggerObserver.Radius = _character.Attack.AttackDistance;
    private void OnObserverTriggerEnter(Collider obj) => _character.Attack.IsActive = true;
    private void OnObserverTriggerExit(Collider obj) => _character.Attack.IsActive = false;
  }
}
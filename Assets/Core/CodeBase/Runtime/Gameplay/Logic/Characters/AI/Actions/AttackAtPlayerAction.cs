using System.Collections.Generic;
using UnityEngine;
using WC.Runtime.Gameplay.Tools;

namespace WC.Runtime.Gameplay.Logic
{
  public class AttackAtPlayerAction : AIActionBase
  {
    private readonly CharacterBase _character;
    private readonly ZoneTrigger _closeCombatTrigger;

    public AttackAtPlayerAction(
      CharacterBase character,
      IReadOnlyDictionary<ZoneTriggerID, ZoneTrigger> triggers)
    {
      _character = character;

      _closeCombatTrigger = triggers[ZoneTriggerID.CloseCombat];
      _closeCombatTrigger.Radius = _character.Attack.AttackDistance;
    }


    protected override void SubscribeUpdates()
    {
      _closeCombatTrigger.TriggerEnter += OnCloseCombatTriggerEnter;
      _closeCombatTrigger.TriggerExit += OnCloseCombatTriggerExit;
    }

    protected override void UnsubscribeUpdates()
    {
      _closeCombatTrigger.TriggerEnter -= OnCloseCombatTriggerEnter;
      _closeCombatTrigger.TriggerExit -= OnCloseCombatTriggerExit;
    }
    
    
    private void OnCloseCombatTriggerEnter(Collider target) => _character.Attack.IsActive = true;
    private void OnCloseCombatTriggerExit(Collider target) => _character.Attack.IsActive = false;
  }
}
using System;
using UnityEngine;

namespace WC.Runtime.Logic.Characters
{
  public class CharacterAnimationObserver : MonoBehaviour
  {
    public event Action Attack, AttackEnd;


    private void OnAttack() => 
      Attack?.Invoke();

    private void OnAttackEnd() => 
      AttackEnd?.Invoke();
  }
}
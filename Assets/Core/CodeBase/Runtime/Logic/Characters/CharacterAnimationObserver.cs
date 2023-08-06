using System;
using UnityEngine;

namespace WC.Runtime.Logic.Characters
{
  public class CharacterAnimationObserver : MonoBehaviour
  {
    public event Action Attack;


    private void OnAttack() => 
      Attack?.Invoke();
  }
}
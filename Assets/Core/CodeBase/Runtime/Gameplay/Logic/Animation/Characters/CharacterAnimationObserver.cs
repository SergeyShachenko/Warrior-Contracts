using System;
using UnityEngine;

namespace WC.Runtime.Gameplay.Logic
{
  public class CharacterAnimationObserver : MonoBehaviour
  {
    public event Action Attack;


    private void OnAttack() => 
      Attack?.Invoke();
  }
}
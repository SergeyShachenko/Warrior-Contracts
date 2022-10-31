using System;
using UnityEngine;

namespace CodeBase.Logic.Characters.Enemy
{
  public class EnemyHealth : MonoBehaviour, 
    IHealth
  {
    public event Action HealthChanged;

    public float Max => _max;
    public float Current => _current;

    [SerializeField] private float _max, _current;
     
    [Header("Links")]
    [SerializeField] private EnemyAnimator _enemyAnimator;
    

    public void TakeDamage(float damage)
    {
      if (_current <= 0) return;
      
      
      _current -= damage;
      _enemyAnimator.PlayHit();
      
      HealthChanged?.Invoke();
    }
  }
}
using System;
using UnityEngine;

namespace CodeBase.Logic.Characters
{
  public class EnemyHealth : MonoBehaviour, 
    IHealth
  {
    public event Action HealthChanged;

    public float Max
    {
      get => _max;
      set => _max = value;
    }

    public float Current
    {
      get => _current;
      set => _current = value;
    }

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
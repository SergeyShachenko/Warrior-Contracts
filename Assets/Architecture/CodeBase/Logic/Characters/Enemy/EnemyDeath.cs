using System;
using System.Collections;
using UnityEngine;

namespace CodeBase.Logic.Characters.Enemy
{
  public class EnemyDeath : MonoBehaviour
  {
    public event Action Happened;
    
    [Header("Links")]
    [SerializeField] private EnemyHealth _enemyHealth;
    [SerializeField] private EnemyAnimator _enemyAnimator;


    private void Start() => 
      _enemyHealth.HealthChanged += OnHealthChanged;

    private void OnDestroy() => 
      _enemyHealth.HealthChanged -= OnHealthChanged;

    
    private void OnHealthChanged()
    {
      if (_enemyHealth.Current <= 0) 
        Die();
    }

    private void Die()
    {
      _enemyHealth.HealthChanged -= OnHealthChanged;
      
      _enemyAnimator.PlayDeath();
      StartCoroutine(DestroyBody());
      
      Happened?.Invoke();
    }

    private IEnumerator DestroyBody()
    {
      yield return new WaitForSeconds(3);
      
      
      Destroy(gameObject);
    }
  }
}
using System;
using System.Collections;
using UnityEngine;

namespace CodeBase.Logic.Characters.Enemy
{
  public class EnemyDeath : MonoBehaviour,
    IDeath
  {
    public event Action Happened;
    
    public bool IsDead { get; private set; }

    [Header("Links")]
    [SerializeField] private EnemyHealth _enemyHealth;
    [SerializeField] private EnemyAnimator _enemyAnimator;


    private void Start() => 
      _enemyHealth.HealthChanged += OnHealthChanged;

    private void OnDestroy() => 
      _enemyHealth.HealthChanged -= OnHealthChanged;

    
    private void OnHealthChanged()
    {
      if (IsDead == false && _enemyHealth.Current <= 0) 
        Die();
    }

    private void Die()
    {
      IsDead = true;
      
      _enemyAnimator.PlayDeath();
      StartCoroutine(DestroyBody());
      
      _enemyHealth.HealthChanged -= OnHealthChanged;
      
      Happened?.Invoke();
    }

    private IEnumerator DestroyBody()
    {
      yield return new WaitForSeconds(3);
      
      
      Destroy(gameObject);
    }
  }
}
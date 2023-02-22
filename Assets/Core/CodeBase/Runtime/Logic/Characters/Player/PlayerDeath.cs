using UnityEngine;
using WC.Runtime.Logic.Characters;

namespace WC.Runtime.Logic.Characters
{
  public class PlayerDeath : MonoBehaviour,
    IDeath
  {
    public bool IsDead { get; private set; }

    [Header("Links")]
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private PlayerAttack _playerAttack;
    [SerializeField] private PlayerAnimator _playerAnimator;


    private void Start() => 
      SubscribeToEvents();

    private void OnDestroy() => 
      UnsubscribeToEvents();

    
    private void SubscribeToEvents()
    {
      _playerHealth.HealthChanged += OnHealthChanged;
    }

    private void UnsubscribeToEvents()
    {
      _playerHealth.HealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged()
    {
      if (IsDead == false && _playerHealth.Current <= 0) 
        Die();
    }

    private void Die()
    {
      UnsubscribeToEvents();
      
      IsDead = true;

      _playerAttack.IsActive = false;
      _playerMover.IsActive = false;
      _playerAnimator.PlayDeath();
    }
  }
}
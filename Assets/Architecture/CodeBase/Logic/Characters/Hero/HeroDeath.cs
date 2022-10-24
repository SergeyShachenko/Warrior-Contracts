using UnityEngine;

namespace CodeBase.Logic.Characters.Hero
{
  public class HeroDeath : MonoBehaviour
  {
    public bool IsDead { get; private set; }

    [Header("Links")]
    [SerializeField] private HeroHealth _heroHealth;
    [SerializeField] private HeroMover _heroMover;
    [SerializeField] private HeroAttack _heroAttack;
    [SerializeField] private HeroAnimator _heroAnimator;


    private void Start() => 
      SubscribeToEvents();

    private void OnDestroy() => 
      UnsubscribeToEvents();

    
    private void SubscribeToEvents()
    {
      _heroHealth.HealthChanged += OnHealthChanged;
    }

    private void UnsubscribeToEvents()
    {
      _heroHealth.HealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged()
    {
      if (IsDead == false && _heroHealth.Current <= 0) 
        Die();
    }

    private void Die()
    {
      UnsubscribeToEvents();
      
      IsDead = true;

      _heroAttack.IsActive = false;
      _heroMover.IsActive = false;
      _heroAnimator.PlayDeath();
    }
  }
}
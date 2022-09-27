using UnityEngine;

namespace CodeBase.Logic.Hero
{
  public class HeroDeath : MonoBehaviour
  {
    public bool IsDead { get; private set; }

    [Header("Links")]
    [SerializeField] private HeroHealth _heroHealth;
    [SerializeField] private HeroMover _heroMover;
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
      IsDead = true;
      
      _heroMover.IsActive = false;
      _heroAnimator.PlayDeath();
    }
  }
}
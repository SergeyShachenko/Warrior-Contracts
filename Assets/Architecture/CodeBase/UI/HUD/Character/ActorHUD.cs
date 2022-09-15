using CodeBase.Logic.Hero;
using UnityEngine;

namespace CodeBase.UI.HUD.Character
{
  public class ActorHUD : MonoBehaviour
  {
    [SerializeField] private HPBar _hpBar;

    private HeroHealth _heroHealth;
    
    public void Construct(HeroHealth heroHealth)
    {
      _heroHealth = heroHealth;
      _heroHealth.HealthChanged += OnHealthChanged;
    }


    private void OnDestroy() => 
      _heroHealth.HealthChanged -= OnHealthChanged;


    private void OnHealthChanged()
    {
      _hpBar.SetProgress(_heroHealth.Current, _heroHealth.Max);
    }
  }
}
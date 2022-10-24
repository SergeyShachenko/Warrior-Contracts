using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.UI.HUD.Character
{
  public class ActorHUD : MonoBehaviour
  {
    [SerializeField] private HPBar _hpBar;

    private IHealth _health;
    
    public void Construct(IHealth heroHealth)
    {
      _health = heroHealth;
      _health.HealthChanged += OnHealthChanged;
    }


    private void OnDestroy() => 
      _health.HealthChanged -= OnHealthChanged;


    private void OnHealthChanged()
    {
      _hpBar.SetProgress(_health.Current, _health.Max);
    }
  }
}
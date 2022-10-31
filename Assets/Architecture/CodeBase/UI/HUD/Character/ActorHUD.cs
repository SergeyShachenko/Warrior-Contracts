using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.UI.HUD.Character
{
  public class ActorHUD : MonoBehaviour
  {
    [SerializeField] private HPBar _hpBar;

    private IHealth _health;
    
    public void Construct(IHealth health)
    {
      _health = health;
      _health.HealthChanged += OnHealthChanged;
    }


    private void OnDestroy()
    {
      if (_health != null)
        _health.HealthChanged -= OnHealthChanged;
    }


    private void OnHealthChanged()
    {
      _hpBar.SetProgress(_health.Current, _health.Max);
    }
  }
}
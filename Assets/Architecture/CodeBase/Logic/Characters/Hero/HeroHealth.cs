using System;
using CodeBase.Data;
using CodeBase.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.Logic.Characters.Hero
{
  public class HeroHealth : MonoBehaviour,
    IHealth,
    ISaverProgress
  {
    public event Action HealthChanged;
    
    public float Current
    {
      get => _heroState.CurrentHP;
      set
      {
        if (_heroState.CurrentHP == value) return;
        
        
        _heroState.CurrentHP = value;
        HealthChanged?.Invoke();
      }
    }

    public float Max
    {
      get => _heroState.MaxHP;
      set
      {
        if (_heroState.MaxHP == value) return;
        
        
        _heroState.MaxHP = value;
        HealthChanged?.Invoke();
      }
    }

    [Header("Links")]
    [SerializeField] private HeroAnimator _heroAnimator;
    
    private HeroState _heroState;


    public void LoadProgress(PlayerProgress progress)
    {
      _heroState = progress.HeroState;
      HealthChanged?.Invoke();
    }

    public void SaveProgress(PlayerProgress progress)
    {
      progress.HeroState.CurrentHP = Current;
      progress.HeroState.MaxHP = Max;
    }

    public void TakeDamage(float damage)
    {
      if(Current <= 0) return;
      
      
      Current -= damage;
      _heroAnimator.PlayHit();
    }
  }
}
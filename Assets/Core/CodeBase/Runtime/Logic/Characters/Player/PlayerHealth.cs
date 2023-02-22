using System;
using UnityEngine;
using WC.Runtime.Data.Characters;
using WC.Runtime.Infrastructure.Services;
using WC.Runtime.Logic.Characters;

namespace WC.Runtime.Logic.Characters
{
  public class PlayerHealth : MonoBehaviour,
    IHealth,
    ISaverProgress
  {
    public event Action HealthChanged;
    
    public float Current
    {
      get => _playerStateData.CurrentHP;
      set
      {
        if (_playerStateData.CurrentHP == value) return;
        
        
        _playerStateData.CurrentHP = value;
        HealthChanged?.Invoke();
      }
    }

    public float Max
    {
      get => _playerStateData.MaxHP;
      set
      {
        if (_playerStateData.MaxHP == value) return;
        
        
        _playerStateData.MaxHP = value;
        HealthChanged?.Invoke();
      }
    }

    [Header("Links")]
    [SerializeField] private PlayerAnimator _playerAnimator;
    
    private PlayerStateData _playerStateData;


    public void LoadProgress(PlayerProgressData progressData)
    {
      _playerStateData = progressData.State;
      HealthChanged?.Invoke();
    }

    public void SaveProgress(PlayerProgressData progressData)
    {
      progressData.State.CurrentHP = Current;
      progressData.State.MaxHP = Max;
    }

    public void TakeDamage(float damage)
    {
      if(Current <= 0) return;
      
      
      Current -= damage;
      _playerAnimator.PlayHit();
    }
  }
}
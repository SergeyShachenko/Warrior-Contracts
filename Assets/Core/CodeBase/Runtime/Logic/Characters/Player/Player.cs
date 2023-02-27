using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using WC.Runtime.Data;
using WC.Runtime.Data.Characters;
using WC.Runtime.Infrastructure.Services;

namespace WC.Runtime.Logic.Characters
{
  public class Player : MonoBehaviour,
    ICharacter,
    ISaverProgress
  {
    public event Action Initialized;
    
    public IHealth Health => _health;
    public IDeath Death => _death;
    public IAttack Attack => _attack;
    public PlayerMovement Movement => _movement;
    public ICharacterAnimator Animator => _charAnimator;

    [SerializeField] private CharacterController _controller;
    [SerializeField] private Animator _animator;
    [SerializeField] private CharacterAnimationObserver _animObserver;

    private IInputService _inputService;
    
    private PlayerProgressData _progress;
    private PlayerHealth _health;
    private PlayerDeath _death;
    private PlayerAttack _attack;
    private PlayerMovement _movement;
    private PlayerAnimator _charAnimator;

    private bool _initialized;

    public void Construct(IInputService inputService) => 
      _inputService = inputService;

    private void Init()
    {
      _health = new PlayerHealth(_progress);
      _death = new PlayerDeath();
      _attack = new PlayerAttack(_inputService, _progress, _controller, transform);
      _movement = new PlayerMovement(_inputService, _progress, _controller, transform);
      _charAnimator = new PlayerAnimator(_controller, _animator);
      
      _health.TakingDamage += OnTakeDamage;
      _health.Changed += OnHealthChanged;
      _death.Happened += OnDeath;
      _attack.Attack += OnAttack;
      _animObserver.Attack += OnAnimAttack;
      _animObserver.AttackEnd += OnAnimAttackEnd;

      _initialized = true;
      Initialized?.Invoke();
    }

    
    private void Update()
    {
      if (_initialized == false) return;
      if (_death.IsDead) return;
      
      
      if (_charAnimator.IsAttacking == false) 
        _attack.Tick();

      _movement.Tick();
      _charAnimator.Tick();
    }

    private void OnDestroy()
    {
      _health.TakingDamage -= OnTakeDamage;
      _health.Changed -= OnHealthChanged;
      _death.Happened -= OnDeath;
      _attack.Attack -= OnAttack;
      _animObserver.Attack -= OnAnimAttack;
      _animObserver.AttackEnd -= OnAnimAttackEnd;
    }
    

    private void OnTakeDamage()
    {
      _charAnimator.PlayHit();
    }

    private void OnHealthChanged()
    {
      _death.CheckDeath(_health.Current);
    }

    private void OnDeath()
    {
      _charAnimator.PlayDeath();
    }

    private void OnAttack()
    {
      _charAnimator.PlayAttack();
    }

    private void OnAnimAttack()
    {
      _attack.TakeDamage();
    }

    private void OnAnimAttackEnd()
    {
      
    }

    void ILoaderProgress.LoadProgress(PlayerProgressData progressData)
    {
      _progress = progressData;
      Init();
    }

    void ISaverProgress.SaveProgress(PlayerProgressData progressData)
    {
      progressData.State.CurrentHP = _health.Current;
      progressData.State.MaxHP = _health.Max;
      progressData.World.LevelPos = new LevelPositionData(SceneManager.GetActiveScene().name, transform.position.ToVector3Data());
    }
  }
}
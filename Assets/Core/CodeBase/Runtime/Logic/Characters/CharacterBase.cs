using System;
using UnityEngine;

namespace WC.Runtime.Logic.Characters
{
  public abstract class CharacterBase : MonoBehaviour,
    ICharacter
  {
    public event Action Initialized;
    
    public IAttack Attack { get; protected set; }
    public IHealth Health { get; protected set; }
    public IDeath Death { get; protected set; }
    public IMovement Movement { get; protected set; }
    public ICharacterAnimator Animator { get; protected set; }
    
    [SerializeField] protected CharacterController _controller;
    [SerializeField] protected Animator _animator;
    [SerializeField] protected CharacterAnimationObserver _animObserver;

    private bool _initialized;


    protected virtual void Update()
    {
      if (_initialized == false) return;
      if (Death.IsDead) return;
      
      
      if (Animator is { IsAttacking: false }) 
        Attack?.Tick();
      
      Animator?.Tick();
      Movement?.Tick();
    }

    protected virtual void OnDestroy() => UnsubscribeUpdates();

    
    protected virtual void Init()
    {
      SubscribeUpdates();
      _initialized = true;
      Initialized?.Invoke();
    }

    protected virtual void SubscribeUpdates()
    {
      Health.TakingDamage += OnTakeDamage;
      Health.Changed += OnHealthChanged;
      Death.Happened += OnDeath;
      Attack.Attack += OnAttack;
      
      _animObserver.Attack += OnAnimAttack;
      _animObserver.AttackEnd += OnAnimAttackEnd;
    }

    protected virtual void UnsubscribeUpdates()
    {
      Health.TakingDamage -= OnTakeDamage;
      Health.Changed -= OnHealthChanged;
      Death.Happened -= OnDeath;
      Attack.Attack -= OnAttack;
      
      _animObserver.Attack -= OnAnimAttack;
      _animObserver.AttackEnd -= OnAnimAttackEnd;
    }

    protected virtual void OnTakeDamage() => Animator.PlayHit();
    protected virtual void OnHealthChanged() => Death.CheckDeath(Health.Current);
    protected virtual void OnDeath() => Animator.PlayDeath();
    protected virtual void OnAttack() => Animator.PlayAttack();
    protected virtual void OnAnimAttack() => Attack.TakeDamage();
    protected virtual void OnAnimAttackEnd() => Attack.StopAttack();
  }
}
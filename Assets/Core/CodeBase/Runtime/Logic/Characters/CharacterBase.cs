using System;
using UnityEngine;
using WC.Runtime.Infrastructure;
using WC.Runtime.Infrastructure.Services;
using Zenject;

namespace WC.Runtime.Logic.Characters
{
  public abstract class CharacterBase : MonoBehaviour,
    ICharacter,
    IInitializing
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
    
    private bool _wasInit;

    [Inject]
    private void Construct(ICharacterInitService initService) => initService.Register(this);


    void IInitializing.Initialize()
    {
      Init();
      SubscribeUpdates();
      _wasInit = true;
      Initialized?.Invoke();
    }
    
    protected virtual void FixedUpdate()
    {
      if (_wasInit && Death.IsDead == false)
        Tick();
    }

    protected virtual void OnDestroy()
    {
      if (_wasInit)
        UnsubscribeUpdates();
    }

    
    protected virtual void Init(){}
    
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

    protected virtual void Tick()
    {
      if (Animator is { IsAttacking: false }) 
        Attack?.Tick();
      
      Animator?.Tick();
      Movement?.Tick();
    }

    protected virtual void OnTakeDamage() => Animator.PlayHit();
    protected virtual void OnHealthChanged() => Death.CheckDeath(Health.Current);
    protected virtual void OnDeath() => Animator.PlayDeath();
    protected virtual void OnAttack() => Animator.PlayAttack();
    protected virtual void OnAnimAttack() => Attack.TakeDamage();
    protected virtual void OnAnimAttackEnd() => Attack.StopAttack();
  }
}
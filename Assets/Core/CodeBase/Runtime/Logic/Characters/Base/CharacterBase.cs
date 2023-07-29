using System;
using UnityEngine;
using WC.Runtime.Infrastructure;
using WC.Runtime.Infrastructure.Services;
using Zenject;
using Random = UnityEngine.Random;

namespace WC.Runtime.Logic.Characters
{
  public abstract class CharacterBase : MonoBehaviour,
    IInitializing
  {
    public event Action Initialized;
    
    public CharacterAttackBase Attack { get; protected set; }
    public CharacterHealthBase Health { get; protected set; }
    public CharacterDeathBase Death { get; protected set; }
    public CharacterMovementBase Movement { get; protected set; }
    public CharacterAnimatorBase Animator { get; protected set; }

    [SerializeField] protected Animator p_Animator;
    [SerializeField] protected CharacterAnimationObserver p_AnimationObserver;
    
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
      
      p_AnimationObserver.Attack += OnAnimAttack;
      p_AnimationObserver.AttackEnd += OnAnimAttackEnd;
    }

    protected virtual void UnsubscribeUpdates()
    {
      Health.TakingDamage -= OnTakeDamage;
      Health.Changed -= OnHealthChanged;
      Death.Happened -= OnDeath;
      Attack.Attack -= OnAttack;
      
      p_AnimationObserver.Attack -= OnAnimAttack;
      p_AnimationObserver.AttackEnd -= OnAnimAttackEnd;
    }

    protected virtual void Tick()
    {
      if (Animator is { IsAttacking: false }) 
        Attack?.Tick();
      
      Animator?.Tick();
      Movement?.Tick();
    }

    protected virtual void OnTakeDamage() => Animator.PlayHit(id: 1);
    protected virtual void OnHealthChanged() => Death.CheckDeath(Health.Current);
    protected virtual void OnDeath() => Animator.PlayDeath(id: Random.Range(1, 6));
    protected virtual void OnAttack() => Animator.PlayAttack(id: 1);
    protected virtual void OnAnimAttack() => Attack.TakeDamage();
    protected virtual void OnAnimAttackEnd() => Attack.StopAttack();
  }
}
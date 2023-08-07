using System;
using UnityEngine;
using WC.Runtime.Infrastructure;
using WC.Runtime.Infrastructure.Services;
using Zenject;
using AnimationState = WC.Runtime.Gameplay.Logic.AnimationState;
using Random = UnityEngine.Random;

namespace WC.Runtime.Gameplay.Logic
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
    
    protected virtual void Update()
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
    }

    protected virtual void UnsubscribeUpdates()
    {
      Health.TakingDamage -= OnTakeDamage;
      Health.Changed -= OnHealthChanged;
      Death.Happened -= OnDeath;
      Attack.Attack -= OnAttack;

      p_AnimationObserver.Attack -= OnAnimAttack;
    }

    protected virtual void Tick()
    {
      Movement.Tick();
      
      if (Animator.CurrentState != AnimationState.Attack) 
        Attack.Tick();
      
      Animator.Tick();
      Animator.PlayAim(Attack.IsAiming);
      Animator.PlayMove(Movement.LocalDirection, Movement.CurrentState);
    }
    
    
    protected virtual void OnTakeDamage()
    {
      Attack.Stop();
      Animator.PlayHit(id: 1);
    }

    protected virtual void OnHealthChanged() => Death.CheckDeath(Health.Current);
    protected virtual void OnDeath() => Animator.PlayDeath(id: Random.Range(1, 6));
    protected virtual void OnAttack() => Animator.PlayAttack(id: 1);
    protected virtual void OnAnimAttack() => Attack.DoDamage();
  }
}
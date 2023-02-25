using System;
using System.Collections;
using UnityEngine;

namespace WC.Runtime.Logic.Characters
{
  public class Enemy : MonoBehaviour,
    ICharacter
  {
    public event Action Initialized;
    
    public IAttack Attack => _attack;
    public IHealth Health => _health;
    public IDeath Death => _death;
    public ICharacterAnimator Animator => _charAnimator;
    
    [SerializeField] private Animator _animator;
    [SerializeField] private CharacterAnimationObserver _animObserver;

    private EnemyHealth _health;
    private EnemyDeath _death;
    private EnemyAttack _attack;
    private ICharacterAnimator _charAnimator;
    
    private Player _player;

    private float _currentHP;
    private float _maxHP;
    private float _damage;
    private float _attackDistance;
    private float _hitRadius;
    private float _cooldown;
    private bool _initialized;

    public void Construct(
      Player player,
      float currentHP,
      float maxHP,
      float damage,
      float attackDistance,
      float hitRadius,
      float cooldown)
    {
      _player = player;
      
      _currentHP = currentHP;
      _maxHP = maxHP;
      
      _damage = damage;
      _attackDistance = attackDistance;
      _hitRadius = hitRadius;
      _cooldown = cooldown;
      
      Init();
    }
    
    private void Init()
    {
      _health = new EnemyHealth(_currentHP, _maxHP);
      _death = new EnemyDeath();
      _attack = new EnemyAttack(_player, transform, _damage, _attackDistance, _hitRadius, _cooldown);
      _charAnimator = new EnemyAnimator(_animator);
      
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
      
      
      _attack.Tick();
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
      StartCoroutine(DestroyBody());
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
      _attack.StopAttack();
    }
    
    private IEnumerator DestroyBody()
    {
      yield return new WaitForSeconds(3);

      Destroy(gameObject);
    }
  }
}
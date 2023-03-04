using System.Collections;
using UnityEngine;

namespace WC.Runtime.Logic.Characters
{
  public class Enemy : CharacterBase
  {
    private Player _player;

    private float _currentHP;
    private float _maxHP;
    private float _damage;
    private float _attackDistance;
    private float _hitRadius;
    private float _cooldown;

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

    protected override void Init()
    {
      Health = new EnemyHealth(_currentHP, _maxHP);
      Death = new EnemyDeath();
      Attack = new EnemyAttack(_player, transform, _damage, _attackDistance, _hitRadius, _cooldown);
      Animator = new EnemyAnimator(_animator);
      
      base.Init();
    }
    

    protected override void OnDeath()
    {
      base.OnDeath();
      StartCoroutine(DestroyBody());
    }

    private IEnumerator DestroyBody()
    {
      yield return new WaitForSeconds(3);

      Destroy(gameObject);
    }
  }
}
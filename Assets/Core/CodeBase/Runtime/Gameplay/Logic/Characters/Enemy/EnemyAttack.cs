﻿using System.Linq;
using UnityEngine;
using WC.Runtime.Gameplay.Data;
using WC.Runtime.DebugTools;

namespace WC.Runtime.Gameplay.Logic
{
  public class EnemyAttack : CharacterAttackBase
  {
    private const float HitDebugDuration = 1f;

    private readonly Enemy _enemy;
    private readonly Transform _transform;
    private readonly int _layerMask;

    private Collider[] _hits = new Collider[1];

    private float _attackCooldownCounter;
    private bool _isAttack;

    public EnemyAttack(Enemy enemy, CombatStatsData data) : base(enemy, data)
    {
      _enemy = enemy;
      _transform = enemy.transform;
      _layerMask = 1 << LayerMask.NameToLayer("Player");
      
      IsActive = false;
    }


    public override void Tick()
    {
      if (IsActive == false) return;

      
      UpdateCooldown();

      if (CanAttack())
        Start();
    }

    
    public override void DoDamage()
    {
      if (Hit(out Collider hit))
      {
        CustomGizmos.DrawSphere(GetHitPoint(), HitRadius, HitDebugDuration);
        hit.transform.GetComponent<Player>().Health.TakeDamage(Damage);
      }
      
      Stop();
    }

    
    protected override void Start()
    {
      base.Start();
      _isAttack = true;
    }

    public override void Stop()
    {
      base.Stop();
      
      _attackCooldownCounter = Cooldown;
      _isAttack = false;
    }

    private void UpdateCooldown()
    {
      if (_attackCooldownCounter > 0)
        _attackCooldownCounter -= Time.deltaTime;
    }

    private bool Hit(out Collider hit)
    {
      int hitsCount = Physics.OverlapSphereNonAlloc(GetHitPoint(), HitRadius, _hits, _layerMask);
      hit = _hits.FirstOrDefault();
      
      return hitsCount > 0;
    }

    private Vector3 GetHitPoint()
    {
      Vector3 currentPos = _transform.position;
      var newPos = new Vector3(currentPos.x, currentPos.y + 0.5f, currentPos.z);
      
      return newPos + _transform.forward * AttackDistance;
    }

    private bool CanAttack() => 
      _isAttack == false && _attackCooldownCounter <= 0;
  }
}
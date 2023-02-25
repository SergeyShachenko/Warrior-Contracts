using System;
using System.Linq;
using UnityEngine;
using WC.Runtime.DebugTools;

namespace WC.Runtime.Logic.Characters
{
  public class EnemyAttack : IAttack
  {
    public event Action Attack;

    public bool IsActive { get; set; } = false;
    public float Damage { get; }
    public float AttackDistance { get; }
    public float Cooldown { get; }
    public float HitRadius { get; }

    private const float HitDebugDuration = 1f;

    private readonly Player _player;
    private readonly Transform _transform;
    private readonly int _layerMask;

    private Collider[] _hits = new Collider[1];

    private float _attackCooldownCounter;
    private bool _isAttack;

    public EnemyAttack(
      Player player, 
      Transform transform,
      float damage,
      float attackDistance,
      float hitRadius, 
      float cooldown)
    {
      _player = player;
      _transform = transform;
      
      Damage = damage;
      AttackDistance = attackDistance;
      HitRadius = hitRadius;
      Cooldown = cooldown;
      
      _layerMask = 1 << LayerMask.NameToLayer("Player");
    }


    public void Tick()
    {
      if (IsActive == false) return;

      
      UpdateCooldown();

      if (CanAttack())
        StartAttack();
    }

    public void TakeDamage()
    {
      if (IsActive == false) return;
      
      
      if (Hit(out Collider hit))
      {
        PhysicsDebug.DrawSphere(GetHitPoint(), HitRadius, HitDebugDuration);
        hit.transform.GetComponent<Player>().Health.TakeDamage(Damage);
      }
    }

    public void StopAttack()
    {
      if (IsActive == false) return;
      
      
      _attackCooldownCounter = Cooldown;
      _isAttack = false;
    }

    private void UpdateCooldown()
    {
      if (_attackCooldownCounter > 0)
        _attackCooldownCounter -= Time.deltaTime;
    }

    private void StartAttack()
    {
      _transform.LookAt(_player.transform);
      _isAttack = true;
      Attack?.Invoke();
    }

    private bool Hit(out Collider hit)
    {
      var hitsCount = Physics.OverlapSphereNonAlloc(GetHitPoint(), HitRadius, _hits, _layerMask);
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
      _isAttack == false && _player.Death.IsDead == false && _attackCooldownCounter <= 0;
  }
}
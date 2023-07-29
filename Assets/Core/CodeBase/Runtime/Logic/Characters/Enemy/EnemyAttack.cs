using System.Linq;
using UnityEngine;
using WC.Runtime.Data.Characters;
using WC.Runtime.DebugTools;

namespace WC.Runtime.Logic.Characters
{
  public class EnemyAttack : CharacterAttackBase
  {
    private const float HitDebugDuration = 1f;

    private readonly Enemy _enemy;
    private readonly Transform _transform;
    private readonly Player _player;
    private readonly int _layerMask;

    private Collider[] _hits = new Collider[1];

    private float _attackCooldownCounter;
    private bool _isAttack;

    public EnemyAttack(
      Enemy enemy,
      CombatStatsData data,
      Player player)
    : base(enemy, data)
    {
      _enemy = enemy;
      _transform = enemy.transform;
      _player = player;
      _layerMask = 1 << LayerMask.NameToLayer("Player");
      
      IsActive = false;
    }


    public override void Tick()
    {
      if (IsActive == false) return;

      
      UpdateCooldown();

      if (CanAttack())
        StartAttack();
    }

    public override void TakeDamage()
    {
      if (IsActive == false) return;
      
      
      if (Hit(out Collider hit))
      {
        PhysicsDebug.DrawSphere(GetHitPoint(), HitRadius, HitDebugDuration);
        hit.transform.GetComponent<Player>().Health.TakeDamage(Damage);
      }
    }

    public override void StopAttack()
    {
      if (IsActive == false) return;
      
      
      _attackCooldownCounter = Cooldown;
      _isAttack = false;
    }

    protected override void StartAttack()
    {
      base.StartAttack();
      
      _transform.LookAt(_player.transform);
      _isAttack = true;
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
      _isAttack == false && _player.Death.IsDead == false && _attackCooldownCounter <= 0;
  }
}
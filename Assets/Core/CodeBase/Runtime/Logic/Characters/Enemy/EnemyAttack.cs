using System.Linq;
using CodeBase.Tools;
using UnityEngine;

namespace CodeBase.Logic.Characters
{
  public class EnemyAttack : MonoBehaviour
  {
    public bool IsActive { get; set; }
    
    public float Damage
    {
      get => _damage;
      set => _damage = value;
    }
    public float AttackDistance
    {
      get => _attackDistance;
      set => _attackDistance = value;
    }
    public float AttackCooldown
    {
      get => _attackCooldown;
      set => _attackCooldown = value;
    }
    public float HitRadius
    {
      get => _hitRadius;
      set => _hitRadius = value;
    }

    private const float HitDebugDuration = 1f;

    [SerializeField] private float _damage;
    [SerializeField] private float _attackDistance;
    [SerializeField] private float _hitRadius;
    [SerializeField] private float _attackCooldown;

    [Header("Links")] 
    [SerializeField] private EnemyDeath _enemyDeath;
    [SerializeField] private EnemyAnimator _enemyAnimator;
    
    private PlayerDeath _playerDeath;
    private GameObject _player;
    private Collider[] _hits = new Collider[1];
    private float _attackCooldownCounter;
    private bool _isAttack;
    private int _layerMask;

    public void Construct(GameObject player)
    {
      _player = player;
      _playerDeath = _player.GetComponent<PlayerDeath>();
    }
    
    
    private void Awake()
    {
      _enemyAnimator.Attack += OnAttack;
      _enemyAnimator.AttackEnd += OnAttackEnd;

      _layerMask = 1 << LayerMask.NameToLayer("Player");
    }

    private void Update()
    {
      UpdateAttackCooldown();

      if (CanAttack())
        StartAttack();
    }

    
    private void UpdateAttackCooldown()
    {
      if (_attackCooldownCounter > 0)
        _attackCooldownCounter -= Time.deltaTime;
    }

    private void StartAttack()
    {
      transform.LookAt(_player.transform);
      _enemyAnimator.PlayAttack();

      _isAttack = true;
    }

    private void OnAttack()
    {
      if (Hit(out Collider hit))
      {
        PhysicsDebug.DrawSphere(GetHitPoint(), _hitRadius, HitDebugDuration);
        hit.transform.GetComponent<IHealth>().TakeDamage(_damage);
      }
    }

    private void OnAttackEnd()
    {
      _attackCooldownCounter = _attackCooldown;
      _isAttack = false;
    }

    private bool Hit(out Collider hit)
    {
      var hitsCount = Physics.OverlapSphereNonAlloc(GetHitPoint(), _hitRadius, _hits, _layerMask);
      hit = _hits.FirstOrDefault();
      
      return hitsCount > 0;
    }

    private Vector3 GetHitPoint()
    {
      Vector3 currentPos = transform.position;
      var newPos = new Vector3(currentPos.x, currentPos.y + 0.5f, currentPos.z);
      
      return newPos + transform.forward * _attackDistance;
    }

    private bool CanAttack() => 
      IsActive && _isAttack == false && _playerDeath.IsDead == false && _enemyDeath.IsDead == false && _attackCooldownCounter <= 0;
  }
}
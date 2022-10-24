using System.Linq;
using CodeBase.Infrastructure.Factories;
using CodeBase.Infrastructure.Services;
using CodeBase.Logic.Characters.Enemy;
using CodeBase.Logic.Characters.Hero;
using CodeBase.Tools;
using UnityEngine;

namespace CodeBase.Logic.Characters
{
  public class AttackAI : MonoBehaviour
  {
    public bool IsActive { get; set; }

    private const float HitDebugDuration = 1f;

    [SerializeField] private float _damage = 10f;
    [SerializeField] private float _attackDistance = 2f;
    [SerializeField] private float _hitRadius = 0.5f;
    [SerializeField] private float _attackCooldown = 1f;

    [Header("Links")]
    [SerializeField] private EnemyAnimator _enemyAnimator;

    private IGameFactory _gameFactory;
    private HeroDeath _heroDeath;
    private GameObject _hero;
    private Collider[] _hits = new Collider[1];
    private float _attackCooldownCounter;
    private bool _isAttack;
    private int _layerMask;


    private void Awake()
    {
      _gameFactory = AllServices.Container.Single<IGameFactory>();
      
      _gameFactory.HeroCreate += OnHeroCreate;
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
      transform.LookAt(_hero.transform);
      _enemyAnimator.PlayAttack();

      _isAttack = true;
    }

    private void OnHeroCreate()
    {
      _hero = _gameFactory.Hero;
      _heroDeath = _hero.GetComponent<HeroDeath>();
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
      IsActive && _isAttack == false && _heroDeath.IsDead == false && _attackCooldownCounter <= 0;
  }
}
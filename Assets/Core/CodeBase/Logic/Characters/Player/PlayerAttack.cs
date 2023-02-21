using CodeBase.Data;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.Logic.Characters
{
  public class PlayerAttack : MonoBehaviour,
    ILoaderProgress
  {
    public bool IsActive
    {
      get => enabled;
      set => enabled = value;
    }

    [Header("Links")]
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private PlayerAnimator _playerAnimator;

    private static int layerMask;
    
    private IInputService _inputService;
    private PlayerStatsData _playerStatsData;

    private Collider[] _hits = new Collider[3];


    private void Awake()
    {
      _inputService = AllServices.Container.Single<IInputService>(); 
      layerMask = 1 << LayerMask.NameToLayer("Hittable");
      _playerAnimator.Attack += OnAttack;
    }

    private void Update()
    {
      if (_inputService.GetAttackButtonUp() && _playerAnimator.IsAttacking == false) 
        _playerAnimator.PlayAttack();
    }


    private void OnAttack()
    {
      for (var i = 0; i < Hit(); ++i) 
        _hits[i].transform.parent.parent.GetComponent<IHealth>().TakeDamage(_playerStatsData.Damage);
    }

    private int Hit() => 
      Physics.OverlapSphereNonAlloc(StartPoint() + transform.forward, _playerStatsData.DamageRadius, _hits, layerMask);

    private Vector3 StartPoint() =>
      new Vector3(transform.position.x, _characterController.center.y/2, transform.position.z);
    
    void ILoaderProgress.LoadProgress(PlayerProgressData progressData) => 
      _playerStatsData = progressData.Stats;
  }
}
using UnityEngine;
using UnityEngine.AI;
using WC.Runtime.Gameplay.Data;
using WC.Runtime.Extensions;

namespace WC.Runtime.Gameplay.Logic
{
  public class EnemyMovement : CharacterMovementBase
  {
    private const float MinAgentSpeed = 0.1f;
    private const float DirectionDamping = 2.5f;

    private readonly Enemy _enemy;
    private readonly Transform _transform;
    private readonly NavMeshAgent _agent;

    private Vector3 _moveTargetPos;

    public EnemyMovement(Enemy enemy,
      MovementStatsData data,
      NavMeshAgent agent)
      : base(enemy, data)
    {
      _enemy = enemy;
      _transform = enemy.transform;
      _agent = agent;
    }

    
    public override void Tick()
    {
      if (IsActive == false) return;

      
      if (IsMoving())
      {
        Vector3 targetDirection = (_moveTargetPos - _transform.position).normalized;
        Direction = Vector3.Lerp(Direction, targetDirection, Time.deltaTime * DirectionDamping);

        Vector3 targetLocalDirection = _transform.InverseTransformDirection(targetDirection);
        LocalDirection = Vector3.Lerp(LocalDirection, targetLocalDirection, Time.deltaTime * DirectionDamping);
      }
      else
      {
        EnterToState(MovementState.Idle);

        Direction = Vector3.Lerp(Direction, Vector3.zero, Time.deltaTime * DirectionDamping);
        LocalDirection = Vector3.Lerp(LocalDirection, Vector3.zero, Time.deltaTime * DirectionDamping);
      }
    }


    public override void Move(Vector3 at, MovementState state)
    {
      if (IsActive == false && _agent.enabled == false) return;


      EnterToState(state);
      _moveTargetPos = at;

      _agent.speed = CurrentSpeed;
      _agent.SetDestination(_moveTargetPos);
    }

    public override void Warp(Vector3 to)
    {
      _agent.enabled = false;
      _transform.position = to.AddY(_agent.height);
      _agent.enabled = true;
    }

    public override void WarpToSavedPos()
    {
    }


    private bool IsMoving() =>
      _agent.velocity.magnitude > MinAgentSpeed && _agent.remainingDistance > _agent.stoppingDistance;
  }
}
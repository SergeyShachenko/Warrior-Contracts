using UnityEngine;
using UnityEngine.AI;
using WC.Runtime.Gameplay.Data;
using WC.Runtime.Extensions;

namespace WC.Runtime.Gameplay.Logic
{
  public class EnemyMovement : CharacterMovementBase
  {
    private const float MinAgentSpeed = 0.1f;

    private readonly Enemy _enemy;
    private readonly Transform _transform;
    private readonly NavMeshAgent _agent;
    
    private Vector3 _moveTargetPos;

    public EnemyMovement(
      Enemy enemy,
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
        Direction = (_moveTargetPos - _transform.position).normalized;
        LocalDirection = _transform.InverseTransformDirection(Direction);
      }
      else
      {
        EnterToState(MovementState.Idle);
        
        Direction = Vector3.zero;
        LocalDirection = Vector3.zero;
      }
    }

    
    public override void MoveToTarget(Vector3 position, MovementState state)
    {
      if (IsActive == false && _agent.enabled == false) return;


      EnterToState(state);
      _moveTargetPos = position;
      
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
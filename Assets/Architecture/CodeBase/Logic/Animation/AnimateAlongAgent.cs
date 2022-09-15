using CodeBase.Logic.Enemy;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Logic.Animation
{
  public class AnimateAlongAgent : MonoBehaviour
  {
    private const float MinAgentSpeed = 0.1f;
    
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private EnemyAnimator _enemyAnimator;


    private void Update()
    {
      if (AgentShouldMove())
        _enemyAnimator.Move(_agent.velocity.magnitude);
      else
        _enemyAnimator.StopMove();
    }

    private bool AgentShouldMove() => 
      _agent.velocity.magnitude > MinAgentSpeed && _agent.remainingDistance > _agent.radius;
  }
}
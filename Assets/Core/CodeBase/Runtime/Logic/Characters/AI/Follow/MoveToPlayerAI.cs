using UnityEngine;
using UnityEngine.AI;

namespace WC.Runtime.Logic.Characters
{
  public class MoveToPlayerAI : FollowAIBase
  {
    [SerializeField] private float _minDistanceToPlayer = 2f;
    
    [Header("Links")]
    [SerializeField] private NavMeshAgent _agent;
    
    
    private void Update()
    {
      if (PlayerIsFar() && p_Player.Death.IsDead == false && p_Enemy.Death.IsDead == false)
        _agent.destination = p_Player.transform.position;
    }


    private bool PlayerIsFar() => 
      Vector3.Distance(_agent.transform.position, p_Player.transform.position) >= _minDistanceToPlayer;
  }
}
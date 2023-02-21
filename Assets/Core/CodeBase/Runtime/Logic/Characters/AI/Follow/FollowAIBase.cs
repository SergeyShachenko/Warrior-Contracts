using UnityEngine;

namespace CodeBase.Logic.Characters
{
  public abstract class FollowAIBase : MonoBehaviour
  {
    [Header("Links")]
    [SerializeField] protected EnemyDeath p_EnemyDeath;
    
    protected GameObject p_Player;
    protected PlayerDeath p_PlayerDeath;


    public void Construct(GameObject player)
    {
      p_Player = player;
      p_PlayerDeath = p_Player.GetComponent<PlayerDeath>();
    }
  }
}
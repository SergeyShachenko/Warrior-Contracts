using UnityEngine;

namespace WC.Runtime.Logic.Characters
{
  public abstract class FollowAIBase : MonoBehaviour
  {
    [Header("Links")]
    [SerializeField] protected Enemy p_Enemy;
    
    protected GameObject p_Player;
    protected IDeath p_PlayerDeath;


    public void Init(GameObject player)
    {
      p_Player = player;
      p_PlayerDeath = p_Player.GetComponent<Player>().Death;
    }
  }
}
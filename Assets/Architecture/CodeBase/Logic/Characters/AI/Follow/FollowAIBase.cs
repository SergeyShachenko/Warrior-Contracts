using CodeBase.Logic.Characters.Enemy;
using CodeBase.Logic.Characters.Hero;
using UnityEngine;

namespace CodeBase.Logic.Characters
{
  public abstract class FollowAIBase : MonoBehaviour
  {
    [Header("Links")]
    [SerializeField] protected EnemyDeath p_EnemyDeath;
    
    protected GameObject p_Hero;
    protected HeroDeath p_HeroDeath;


    public void Construct(GameObject hero)
    {
      p_Hero = hero;
      p_HeroDeath = p_Hero.GetComponent<HeroDeath>();
    }
  }
}
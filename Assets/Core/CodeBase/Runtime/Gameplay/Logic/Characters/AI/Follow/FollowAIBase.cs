using UnityEngine;
using WC.Runtime.Gameplay.Services;
using Zenject;

namespace WC.Runtime.Gameplay.Logic
{
  public abstract class FollowAIBase : MonoBehaviour
  {
    [Header("Links")]
    [SerializeField] protected Enemy p_Enemy;
    
    protected Player p_Player;

    [Inject]
    private void Construct(ICharacterFactory characterFactory)
    {
      p_Player = characterFactory.Registry.Player;
    }
  }
}
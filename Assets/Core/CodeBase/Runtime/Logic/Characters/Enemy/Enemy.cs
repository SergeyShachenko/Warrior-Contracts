using System.Collections;
using UnityEngine;
using WC.Runtime.StaticData;

namespace WC.Runtime.Logic.Characters
{
  public class Enemy : CharacterBase
  {
    public EnemyWarriorID ID { get; private set; }
    
    private Player _player;

    private EnemyWarriorStaticData _data;

    public void SetData(Player player, EnemyWarriorStaticData data)
    {
      ID = data.ID;
      
      _player = player;
      _data = data;
    }

    
    protected override void Init()
    {
      Health = new EnemyHealth(_data.Stats.Life);
      Death = new EnemyDeath();
      Attack = new EnemyAttack(this, _data.Stats.Combat, _player);
      Animator = new EnemyAnimator(this, p_Animator);
    }

    protected override void OnDeath()
    {
      base.OnDeath();
      StartCoroutine(DestroyBody());
    }

    
    private IEnumerator DestroyBody()
    {
      yield return new WaitForSeconds(3);

      Destroy(gameObject);
    }
  }
}
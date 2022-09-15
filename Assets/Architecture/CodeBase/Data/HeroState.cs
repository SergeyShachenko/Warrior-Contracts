using System;

namespace CodeBase.Data
{
  [Serializable]
  public class HeroState
  {
    public float MaxHP, CurrentHP;


    public void ResetHP() =>
      CurrentHP = MaxHP;
  }
}
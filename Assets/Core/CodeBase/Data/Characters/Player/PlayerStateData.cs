using System;

namespace CodeBase.Data
{
  [Serializable]
  public class PlayerStateData
  {
    public float MaxHP, CurrentHP;


    public void ResetHP() =>
      CurrentHP = MaxHP;
  }
}
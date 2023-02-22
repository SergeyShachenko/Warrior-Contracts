using System;

namespace WC.Runtime.Data.Characters
{
  [Serializable]
  public class PlayerStateData
  {
    public float MaxHP, CurrentHP;


    public void ResetHP() =>
      CurrentHP = MaxHP;
  }
}
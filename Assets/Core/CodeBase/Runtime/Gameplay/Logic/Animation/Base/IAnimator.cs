using UnityEngine;
using WC.Runtime.Gameplay.Logic;

namespace WC.Runtime.Gameplay.Logic
{
  public interface IAnimator : ILogicComponent
  {
    Animator Animator { get; }
  }
}
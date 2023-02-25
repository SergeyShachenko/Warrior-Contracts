using UnityEngine;
using WC.Runtime.Logic.Characters;

namespace WC.Runtime.Logic
{
  public interface IAnimator : ILogicComponent
  {
    Animator Animator { get; }
  }
}
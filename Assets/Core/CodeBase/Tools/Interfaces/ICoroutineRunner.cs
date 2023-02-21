using System.Collections;
using UnityEngine;

namespace CodeBase.Tools
{
  public interface ICoroutineRunner
  {
    Coroutine StartCoroutine(IEnumerator coroutine);
  }
}
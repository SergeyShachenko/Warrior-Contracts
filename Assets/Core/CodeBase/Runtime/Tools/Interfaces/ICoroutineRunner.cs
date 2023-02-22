using System.Collections;
using UnityEngine;

namespace WC.Runtime.Tools
{
  public interface ICoroutineRunner
  {
    Coroutine StartCoroutine(IEnumerator coroutine);
  }
}
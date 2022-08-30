using System.Collections;
using UnityEngine;

namespace Infrastructure.Tools
{
  public interface ICoroutineRunner
  {
    Coroutine StartCoroutine(IEnumerator coroutine);
  }
}
using System.Collections;
using UnityEngine;

namespace _Game.Scripts.Infrastructure
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}
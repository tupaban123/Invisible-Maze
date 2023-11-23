using System.Collections;

using UnityEngine;

namespace InvisibleMaze.CodeBase
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator enumerator);
    } 
}
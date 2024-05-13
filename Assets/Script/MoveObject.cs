using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    // Startはasyncにできる
    async UniTask Start()
    {
        await transform.DOMoveX(1.0f, 1.0f);
        await transform.DOMoveX(-1.0f, 2.0f);
    }
}


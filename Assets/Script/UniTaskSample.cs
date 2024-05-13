using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using DG.Tweening;
using System.Threading;
using System.Collections.Generic;

public class UniTaskSample : MonoBehaviour
{
    // ---------------------------- SerializeField
    [SerializeField] private float _waitTime;

    [Header("DOTween")]
    [SerializeField] private Type _type;
    [SerializeField] private float _duration;
    [SerializeField] private Ease _ease;
    [SerializeField] private Transform[] _pos;
    [SerializeField] private float _scale;

    // ---------------------------- Field


    // ---------------------------- UnityMessage
    private async void Start()
    {
        //  �����ݒ�
        DG.Tweening.DOTween.SetTweensCapacity(tweenersCapacity: 5000, sequencesCapacity: 200);

        //  ��{�I�ȃ^�X�N
        Debug.Log("��{�I�ȃ^�X�N");
        await UniTask.Delay(TimeSpan.FromSeconds(_waitTime));

        //  �L�����Z����������������^�X�N
        Debug.Log("�L�����Z����������������^�X�N");
        var delayTask = UniTask.Delay(TimeSpan.FromSeconds(_waitTime), cancellationToken: this.destroyCancellationToken);
        if (await delayTask.SuppressCancellationThrow()) { return; }

        //  UniTask�^�̃��\�b�h��Ăяo���^�X�N
        Debug.Log("UniTask�^�̃�b�h��Ăяo���^�X�N");
        var moveTask = MoveTask(this.destroyCancellationToken);
        if (await moveTask.SuppressCancellationThrow()) { return; }

        //  �����Đ�����^�X�N
        Debug.Log("�����Đ�����^�X�N");
        var allTask = AllTask(this.destroyCancellationToken);
        if (await allTask.SuppressCancellationThrow()) { return; }
    }


    // ---------------------------- PublicMethod





    // ---------------------------- PrivateMethod
    /// <summary>
    /// �A�j���[�V��������ɍs���^�X�N
    /// </summary>
    /// <param name="ct"></param>
    /// <returns></returns>
    private async UniTask MoveTask(CancellationToken ct)
    {
        await transform.DOMove(_pos[0].position, _duration)
            .SetLink(gameObject)
            .SetEase(_ease)
            .ToUniTask(TweenCancelBehaviour.KillAndCancelAwait, cancellationToken: ct);

        //await transform.DOMove(_pos[1].position, _duration)
        //    .SetLink(gameObject)
        //    .SetEase(_ease)
        //    .ToUniTask(TweenCancelBehaviour.KillAndCancelAwait, cancellationToken: ct);

        //await transform.DOMove(_pos[2].position, _duration)
        //    .SetLink(gameObject)
        //    .SetEase(_ease)
        //    .ToUniTask(TweenCancelBehaviour.KillAndCancelAwait, cancellationToken: ct);

        //await transform.DOMove(_pos[3].position, _duration)
        //    .SetLink(gameObject)
        //    .SetEase(_ease)
        //    .ToUniTask(TweenCancelBehaviour.KillAndCancelAwait, cancellationToken: ct);
    }

    /// <summary>
    /// �����Đ���s���^�X�N
    /// </summary>
    /// <param name="ct"></param>
    /// <returns></returns>
    private async UniTask AllTask(CancellationToken ct)
    {
        var tasks = new List<UniTask>()
        {
            SingleMoveTask(ct,0),   //  �錾���ɓ����
        };
        tasks.Add(SingleScaleTask(ct)); //  �ォ������

        await UniTask.WhenAll(tasks);   //  �����ɍĐ�����

    }

    /// <summary>
    /// �P��ړ��A�j���[�V����
    /// </summary>
    /// <param name="ct"></param>
    /// <param name="i"></param>
    /// <returns></returns>
    private async UniTask SingleMoveTask(CancellationToken ct, int i)
    {

        await transform.DOMove(_pos[i].position, _duration)
            .SetLink(gameObject)
            .SetEase(_ease)
            .ToUniTask(TweenCancelBehaviour.KillAndCancelAwait, cancellationToken: ct);
    }

    /// <summary>
    /// �P��X�P�[���A�j���[�V����
    /// </summary>
    /// <param name="ct"></param>
    /// <returns></returns>
    private async UniTask SingleScaleTask(CancellationToken ct)
    {
        await transform.DOScale(_scale, _duration)
            .SetLink(gameObject)
            .SetEase(_ease)
            .ToUniTask(TweenCancelBehaviour.KillAndCancelAwait, cancellationToken: ct);
    }
}
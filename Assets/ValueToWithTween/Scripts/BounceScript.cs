using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BounceScript : MonoBehaviour
{
    [SerializeField] private Transform scaleUp;
    [SerializeField] private float scaleValue = 1.1f;
    [SerializeField] private float duration;
    [SerializeField] private Ease ease;

    private Sequence _seq;

    public void StartBounce()
    {
        StopBounce();
        _seq = DOTween.Sequence();
        _seq.Append(scaleUp.DOScale(scaleValue, duration).SetEase(ease).SetLoops(100000, LoopType.Yoyo));
    }

    public void StopBounce()
    {
        _seq.Kill(true);
        scaleUp.transform.localScale = Vector3.one;
    }
}
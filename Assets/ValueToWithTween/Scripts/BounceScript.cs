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
        ResetTween();
        _seq.Append(scaleUp.DOScale(scaleValue, duration)).SetLoops(-1, LoopType.Yoyo).SetEase(ease);
    }

    private void ResetTween()
    {
        _seq.Kill();
        scaleUp.transform.localScale = Vector3.one;
        _seq = DOTween.Sequence();
    }

    public void StopBounce()
    {
        ResetTween();
    }
}
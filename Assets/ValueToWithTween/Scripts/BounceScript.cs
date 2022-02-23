using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using StaticLibrary;
using UnityEngine;

public class BounceScript : MonoBehaviour
{
    [SerializeField] private Transform scaleUp;
    [SerializeField] private float scaleValue = 1.1f;
    [SerializeField] private float duration;
    [SerializeField] private Ease ease;

    public void StartBounce()
    {
        BounceLibrary.StartBounce(scaleUp, scaleValue, duration, ease);
    }

    public void StopBounce()
    {
        BounceLibrary.StopBounce(scaleUp);
    }
}
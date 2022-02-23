using DG.Tweening;
using UnityEngine;

namespace StaticLibrary
{
    public static class BounceLibrary
    {
        private static Sequence _seq;
        private static Vector3 _initialScale;

        public static void StartBounce(Transform bouncer, float scaleValue, float duration, Ease ease)
        {
            _initialScale = bouncer.localScale;
            ResetTween(bouncer);
            _seq.Append(bouncer.DOScale(scaleValue, duration)).SetLoops(-1, LoopType.Yoyo).SetEase(ease);
        }

        private static void ResetTween(Transform bouncer)
        {
            _seq.Kill();
            bouncer.transform.localScale = _initialScale;
            _seq = DOTween.Sequence();
        }

        public static void StopBounce(Transform bouncer)
        {
            ResetTween(bouncer);
        }
    }
}
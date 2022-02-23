using System;
using DG.Tweening;

namespace ValueToTween
{
    public class TweenHelperParam
    {
        public int From;
        public int To;
        public Action OnStart;
        public Action<int> OnUpdate;
        public Action OnComplete;
        public Ease Ease;
        public float Delay = 0;
        public float Duration;
    }

    public static class TweenValueChangeHelper
    {
        private static Sequence _seq;
        private static int _endPoint;

        public static void ChangeValue(TweenHelperParam param)
        {
            _seq.Kill();
            _seq = DOTween.Sequence();

            if (param.Delay > 0)
            {
                float blank = 0;
                float blankEnd = param.Delay;
                _seq.Append(DOTween.To(() => blank, x => blank = x, blankEnd, param.Delay));
                _seq.Append(GetSequence(param));
            }
            else
            {
                _seq.Append(GetSequence(param));
            }
        }

        private static Sequence GetSequence(TweenHelperParam param)
        {
            var seq = DOTween.Sequence();

            int balance = param.From;
            int end = param.To;
            _endPoint = end;
            seq.Append(
                    DOTween.To(() => balance, x => balance = x, end, param.Duration))
                .SetEase(param.Ease)
                .OnStart(()=> param.OnStart?.Invoke())
                .OnUpdate(() =>
                {
                    param.OnUpdate?.Invoke(balance);
                })
                .OnComplete(() =>
                {
                    param.OnComplete?.Invoke();
                });
            seq.Pause();
            return seq;
        }

        public static void ChangeValueInstant(TweenHelperParam param)
        {
            _seq.Kill();
            _seq = DOTween.Sequence();
            _endPoint = param.To;
            param.OnUpdate?.Invoke(_endPoint);
            param.OnComplete?.Invoke();
        }
    }
}
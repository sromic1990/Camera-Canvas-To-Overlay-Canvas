using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

namespace ValueToTween
{
    public class ValueToScript : MonoBehaviour
    {
        [SerializeField] private Ease ease;
        [SerializeField] private float delay;
        [SerializeField] private float duration;

        [SerializeField] private TextMeshProUGUI valueText;
        [SerializeField] private int increment;
        [SerializeField] private Button[] activityButtons;

        public UnityEvent OnStartEvent;
        public UnityEvent OnCompleteEvent;

        private int _currentNumber;
        private Coroutine _updateDelayRoutine;
        private int _numberOfOperations;
        private int _finalValue;

        private void Start()
        {
            _finalValue = _currentNumber;
            Init(0);
        }

        public void AgainInstant()
        {
            Init(0);
        }

        public void UpdateInstant()
        {
            StopAllCoroutinesAndTweens();
            SetNextTarget();
            OnCompleteLoop();
        }

        private void Init(float delay)
        {
            StopAllCoroutinesAndTweens();
            SetNextTarget();
            _updateDelayRoutine = StartCoroutine(UpdateAfterDelay(delay));
        }

        private void SetNextTarget()
        {
            if (_currentNumber == _finalValue)
            {
                _finalValue = _currentNumber + increment;
            }
        }

        private void StopAllCoroutinesAndTweens()
        {
            if (_updateDelayRoutine != null)
            {
                StopCoroutine(_updateDelayRoutine);
            }
            DOTween.KillAll();
        }

        private IEnumerator UpdateAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            UpdateNumbers();
        }

        private void UpdateNumbers()
        {
            float balance = _currentNumber;
            float to = _finalValue;
            DOTween.To(() => balance, x => balance = x, to, duration)
                .SetEase(ease)
                .OnStart(()=> OnStartEvent.Invoke())
                .OnUpdate(() =>
                {
                    UpdateUi((int)balance);
                })
                .OnComplete(OnCompleteLoop);
        }

        private void OnCompleteLoop()
        {
            _numberOfOperations++;
            _currentNumber = _finalValue;
            UpdateUi(_finalValue);
            OnCompleteEvent.Invoke();
            Init(delay);
        }

        private void UpdateUi(int number)
        {
            valueText.text = number.ToString();
            foreach (var button in activityButtons)
            {
                if (_numberOfOperations > 0)
                {
                    button.gameObject.SetActive(true);
                }
                else
                {
                    button.gameObject.SetActive(false);
                }
            }
        }
    }
}
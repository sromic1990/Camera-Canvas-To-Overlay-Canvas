using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using StaticLibrary;
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
            TweenValueChangeHelper.ChangeValueInstant(new TweenHelperParam
            {
                From = _currentNumber,
                To = _finalValue,
                OnUpdate = UpdateUi,
                OnComplete = OnCompleteLoop,
                Ease = ease,
                Delay = 0,
                Duration = duration
            });
        }

        private void Init(float delay)
        {
            SetNextTarget();
            UpdateNumbers(delay);
        }

        private void SetNextTarget()
        {
            if (_numberOfOperations > 0)
            {
                _currentNumber = _finalValue;
            }
            _finalValue += increment;
        }

        private void UpdateNumbers(float delay = 0)
        {
            TweenValueChangeHelper.ChangeValue(new TweenHelperParam
            {
                From = _currentNumber,
                To = _finalValue,
                OnStart = OnStart,
                OnUpdate = OnUpdate,
                OnComplete = OnCompleteLoop,
                Ease = ease,
                Delay = delay,
                Duration = duration
            });
        }

        private void OnStart()
        {
            OnStartEvent.Invoke();
        }

        private void OnCompleteLoop()
        {
            _numberOfOperations++;
            _currentNumber = _finalValue;
            UpdateUi(_finalValue);
            OnCompleteEvent.Invoke();
            Init(delay);
        }

        private void OnUpdate(int number)
        {
            UpdateUi(number);
            if (number == _finalValue)
            {
                OnCompleteEvent.Invoke();
            }
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
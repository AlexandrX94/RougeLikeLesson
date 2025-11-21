using GameCore;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;



    public class GameTimer : MonoBehaviour, IActivate
    {
        [SerializeField] private TMP_Text _gameTimerText;
        private LevelSystem _levelSystem;
        private WaitForSeconds _tick = new WaitForSeconds(1f);
        private Coroutine _timerCoroutine;
        private int _seconds, _minutes;
        public int Minutes => _minutes;

    private void Start()
    {
        Activate();
    }
    public void Activate()
        {
            _timerCoroutine = StartCoroutine(Timer());
        }

        public void Deactivate()
        {
            if (_timerCoroutine != null)
            {
                StopCoroutine(_timerCoroutine);
            }
        }

        private void TimeTextFormat()
        {
            _gameTimerText.text = $"{_minutes}:{_seconds}";
            if (_minutes < 10 && _seconds < 10)
            {
                _gameTimerText.text = $"0{_minutes}:0{_seconds}";
            }
            else if (_seconds < 10)
            {
                _gameTimerText.text = $"{_minutes}:0{_seconds}";
            }
            else if (_minutes < 10)
            {
                _gameTimerText.text = $"0{_minutes}:{_seconds}";
            }
        }


        private IEnumerator Timer()
        {
            while (enabled)
            {
                _seconds++;
                if (_seconds >= 60)
                {
                    _minutes++;
                    _seconds = 0;
                    _levelSystem.OnLevelChanged?.Invoke();
                }
                TimeTextFormat();
                yield return _tick;
            }
        }

        [Inject]
        private void Construct(LevelSystem levelSystem)
        {
            {
                _levelSystem = levelSystem;
            }
        }

    }


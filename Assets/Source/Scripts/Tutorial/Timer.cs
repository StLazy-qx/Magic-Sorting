using Assets.Source.Scripts.MagicCells;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Source.Scripts.Tutorial
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private MagicCellRouter _cellRouter;
        [SerializeField] private float _initialDelay = 1f;
        [SerializeField] private float _repeatDelay = 7f;

        private Coroutine _currentRoutine;

        public event Action SearchRequested;

        private void OnEnable()
        {
            _cellRouter.CellDeparturing += OnCellDeparturing;
        }

        private void OnDisable()
        {
            _cellRouter.CellDeparturing -= OnCellDeparturing;
            StopAllCoroutines();
            _currentRoutine = null;
        }

        private void Start()
        {
            StartTimer(_initialDelay);
        }

        private void OnCellDeparturing()
        {
            StartTimer(_repeatDelay);
        }

        private void StartTimer(float delay)
        {
            if (_currentRoutine != null)
                StopCoroutine(_currentRoutine);

            _currentRoutine = StartCoroutine(TimerRoutine(delay));
        }

        private IEnumerator TimerRoutine(float delay)
        {
            yield return new WaitForSeconds(delay);

            SearchRequested?.Invoke();
            _currentRoutine = null;
        }
    }
}

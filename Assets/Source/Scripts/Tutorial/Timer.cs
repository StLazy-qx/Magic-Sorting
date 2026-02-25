using Assets.Source.Scripts.MagicCells;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Source.Scripts.Tutorial
{
    class Timer : MonoBehaviour
    {
        [SerializeField] private MagicCellRouter _cellRouter;
        [SerializeField] private float _beginSearchInterval = 1f;
        [SerializeField] private float _searchInterval = 7f;

        private WaitForSeconds _beginWaitForSearch;
        private WaitForSeconds _waitForSearch;
        private Coroutine _initialSearchRoutine;
        private Coroutine _delayedSearchRoutine;

        public event Action SearchRequested;

        private void Awake()
        {
            _beginWaitForSearch = new WaitForSeconds(_beginSearchInterval);
            _waitForSearch = new WaitForSeconds(_searchInterval);

            ValidateDependencies();
        }

        private void OnEnable()
        {
            if (_cellRouter != null)
                _cellRouter.CellDeparturing += OnCellDeparturing;
        }

        private void OnDisable()
        {
            if (_cellRouter != null)
                _cellRouter.CellDeparturing -= OnCellDeparturing;

            StopAllCoroutines();

            _initialSearchRoutine = null;
            _delayedSearchRoutine = null;
        }

        private void Start()
        {
            if (_initialSearchRoutine == null)
                _initialSearchRoutine = StartCoroutine(InitialSearchRoutine());
        }

        private void OnCellDeparturing()
        {
            if (_delayedSearchRoutine != null)
                StopCoroutine(_delayedSearchRoutine);

            _delayedSearchRoutine = StartCoroutine(DelayedSearchRoutine());
        }

        private IEnumerator InitialSearchRoutine()
        {
            yield return _beginWaitForSearch;

            OnSearchRequested();

            _initialSearchRoutine = null;
        }

        private IEnumerator DelayedSearchRoutine()
        {
            yield return _waitForSearch;

            OnSearchRequested();

            _delayedSearchRoutine = null;
        }

        private void OnSearchRequested()
        {
            SearchRequested?.Invoke();
        }

        private void ValidateDependencies()
        {
            if (_cellRouter == null)
            {
                throw new InvalidOperationException(
                    $"{nameof(_cellRouter)} не назначен в инспекторе.");
            }
        }
    }
}

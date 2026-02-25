using Assets.Source.Scripts.Factory;
using Assets.Source.Scripts.InteractiveObjects;
using Assets.Source.Scripts.MagicCells;
using Assets.Source.Scripts.Pool;
using Assets.Source.Scripts.Vessels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Source.Scripts.Tutorial
{
    class Seeker : MonoBehaviour
    {
        [SerializeField] private AnimationParticle _animationParticle;
        [SerializeField] private MagicCellRouter _cellRouter;
        [SerializeField] private MagicColumnPool _magicColumnPool;
        [SerializeField] private VesselPool _vesselPool;
        [SerializeField] private VesselFactory _vesselFactory;
        [SerializeField] private WaitingPoint _waitingPoint;
        [SerializeField] private Transform[] _columns;

        private float _beginsearchInterval = 1f;
        private float _searchInterval = 7f;
        private Coroutine _beginSearchRoutine;
        private Coroutine _searchRoutine;
        private WaitForSeconds _waitForSearch;
        private WaitForSeconds _beginWaitForSearch;
        private List<MagicColumn> _currentColumns;
        private List<MonoVessel> _currentVessels;

        private void Awake()
        {
            _currentVessels = new List<MonoVessel>();
            _currentColumns = new List<MagicColumn>();
            _waitForSearch = new WaitForSeconds(_searchInterval);
            _beginWaitForSearch = new WaitForSeconds(_beginsearchInterval);

            ValidateDependencies();
        }

        private void Start()
        {
            if (_beginSearchRoutine == null)
                _beginSearchRoutine = StartCoroutine(BeginSearchRoutine());
        }

        private void OnEnable()
        {
            _cellRouter.CellDeparturing += OnStartSearchLoop;
        }

        private void OnDisable()
        {
            _cellRouter.CellDeparturing -= OnStartSearchLoop;

            StopSearchLoop();
        }

        private void OnStartSearchLoop()
        {
            if (_searchRoutine != null)
                StopCoroutine(_searchRoutine);

            _searchRoutine = StartCoroutine(SearchRoutine());
        }

        private void StopSearchLoop()
        {
            if (_searchRoutine != null)
            {
                StopCoroutine(_searchRoutine);

                _searchRoutine = null;
            }
        }

        private IEnumerator BeginSearchRoutine()
        {
            yield return _beginWaitForSearch;

            PerformAnalysis();

            _beginSearchRoutine = null;
        }

        private IEnumerator SearchRoutine()
        {
            yield return _waitForSearch;

            PerformAnalysis();

            _searchRoutine = null;
        }

        private void PerformAnalysis()
        {
            LoadCurrentColumns();
            LoadCurrentVessels();
            SearchForMatchingCell();
        }

        public void LoadCurrentColumns()
        {
            _currentColumns.Clear();
            _currentColumns.AddRange(_magicColumnPool
                .GetActiveObjects());
        }

        public void LoadCurrentVessels()
        {
            _currentVessels.Clear();

            foreach (var vessel in _vesselFactory.Objects)
            {
                if (vessel.IsActive)
                    _currentVessels.Add(vessel);
            }
        }

        private void SearchForMatchingCell()
        {
            MagicCell firstWrongCell = null;

            foreach (MonoVessel vessel in _currentVessels)
            {
                foreach (MagicColumn column in _currentColumns)
                {
                    MagicCell cell = GetUpperCell(column);

                    if (cell == null)
                        continue;

                    if (cell.Color == vessel.Color)
                    {
                        _animationParticle.Play(cell.transform.position);

                        return;
                    }

                    if (firstWrongCell == null)
                        firstWrongCell = cell;
                }
            }

            HandleNoMatch(firstWrongCell);
        }

        private MagicCell GetUpperCell(MagicColumn column)
        {
            StackMagicCells stack = column.GetComponent<StackMagicCells>();

            return stack?.GetUpperMagicCell();
        }

        private void HandleNoMatch(MagicCell firstWrongCell)
        {
            if (_waitingPoint.IsFreePlace == false)
            {
                _animationParticle.Play(_waitingPoint.CellPosition);

                return;
            }

            if (firstWrongCell != null)
            {
                _animationParticle.Play(firstWrongCell.transform.position);
            }
        }

        private void ValidateDependencies()
        {
            if (_animationParticle == null)
            {
                throw new InvalidOperationException(
                    $"{nameof(_animationParticle)} не назначен в инспекторе.");
            }

            if (_cellRouter == null)
            {
                throw new InvalidOperationException(
                    $"{nameof(_cellRouter)} не назначен в инспекторе.");
            }

            if (_magicColumnPool == null)
            {
                throw new InvalidOperationException(
                    $"{nameof(_magicColumnPool)} не назначен в инспекторе.");
            }

            if (_vesselPool == null)
            {
                throw new InvalidOperationException(
                    $"{nameof(_vesselPool)} не назначен в инспекторе.");
            }

            if (_waitingPoint == null)
            {
                throw new InvalidOperationException(
                    $"{nameof(_waitingPoint)} не назначен или пуст.");
            }
        }
    }
}

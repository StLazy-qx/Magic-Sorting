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
        [SerializeField] private MagicColumnPool _magicColumnPool;
        [SerializeField] private VesselPool _vesselPool;
        [SerializeField] private VesselFactory _vesselFactory;
        [SerializeField] private WaitingPoint _waitingPoint;
        [SerializeField] private Transform[] _columns;

        private float _beginsearchInterval = 2f;
        private float _searchInterval = 10f;
        private MagicCell _cerruntCell;
        private Coroutine _searchRoutine;
        private WaitForSeconds _waitForSearch;
        private List<MagicColumn> _currentColumns;
        private List<Vessel> _currentVessels;

        private void Awake()
        {
            _currentVessels = new List<Vessel>();
            _waitForSearch = new WaitForSeconds(_searchInterval);

            ValidateDependencies();
        }

        private void Start()
        {
            StartSearchLoop();
        }

        private void OnEnable()
        {
            StartSearchLoop();
        }

        private void OnDisable()
        {
            StopSearchLoop();
        }

        private void StartSearchLoop()
        {
            if (_searchRoutine == null)
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

        private IEnumerator SearchRoutine()
        {
            yield return new WaitForSeconds(_beginsearchInterval);
            PerformAnalysis();

            while (true)
            {
                yield return _waitForSearch;

                PerformAnalysis();
            }
        }

        private void PerformAnalysis()
        {
            LoadCurrentColumns();
            LoadCurrentVessels();
            SetCurrentColumnsCondition();
        }

        public void LoadCurrentColumns()
        {
            IReadOnlyList<MagicColumn> columnPool = _magicColumnPool.GetActiveObjects();

            _currentColumns = new List<MagicColumn>(columnPool);
        }

        public void LoadCurrentVessels()
        {
            _currentVessels.Clear();

            IReadOnlyList<Vessel> vessels = _vesselFactory.Objects;

            foreach (var vessel in vessels)
            {
                if (vessel.IsActive)
                    _currentVessels.Add(vessel);
            }
        }

        private void SetCurrentColumnsCondition()
        {
            bool isMatchFound = false;
            MagicCell firstWrongCell = null;

            foreach (Vessel vessel in _currentVessels)
            {
                if (vessel.gameObject.activeSelf == false)
                    continue;

                foreach (MagicColumn column in _currentColumns)
                {
                    StackMagicCells stack = column.GetComponent<StackMagicCells>();

                    FindCorrectMagicCell(stack);

                    if (_cerruntCell == null)
                        continue;

                    if (_cerruntCell.Color == vessel.Color)
                    {
                        _animationParticle.Play(_cerruntCell.transform.position);

                        isMatchFound = true;

                        return;
                    }

                    if (firstWrongCell == null)
                        firstWrongCell = _cerruntCell;
                }

                if (isMatchFound == false)
                    HandleNoMatch(firstWrongCell);
            }
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

        private void FindCorrectMagicCell(StackMagicCells stack)
        {
            _cerruntCell = stack != null
                ? stack.GetUpperMagicCell()
                : null;
        }

        private void ValidateDependencies()
        {
            if (_animationParticle == null)
            {
                throw new InvalidOperationException(
                    $"{nameof(_animationParticle)} не назначен в инспекторе.");
            }

            if (_vesselPool == null)
            {
                throw new InvalidOperationException(
                    $"{nameof(_vesselPool)} не назначен в инспекторе.");
            }

            if (_columns == null || _columns.Length == 0)
            {
                throw new InvalidOperationException(
                    $"{nameof(_columns)} не назначен или пуст.");
            }
        }
    }
}

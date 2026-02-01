using Assets.Source.Scripts.Factory;
using Assets.Source.Scripts.Enums;
using Assets.Source.Scripts.InteractiveObjects;
using Assets.Source.Scripts.ActionsHandlers;
using System.Collections;
using UnityEngine;
using System;

namespace Assets.Source.Scripts.GameBehaviour
{
    public class GameSessionHandler : BaseGameHandler
    {
        [SerializeField] private ColumnsFactory _columnsFactory;
        [SerializeField] private VesselFactory _vesselFactory;
        [SerializeField] private WaitingPoint _waitingPoint;
        [SerializeField] private ClickImpactHandler _clickImpactHandler;

        //зачем?
        public ClickImpactHandler ClickImpactHandler => _clickImpactHandler;

        private void Start()
        {
            ValidateObjects();
        }

        public void BeginNewRound()
        {
            ContinueGame();
            ResetEntity();
            StartCoroutine(BeginRoundRoutine());
        }

        public void IncreaseDifficultyLevel()
        {
            ContinueGame();

            DifficultyLevel current = _difficultyState.CurrentDifficulty;
            DifficultyLevel newLevel = GetIncreasedDifficulty(current);

            if (newLevel != current)
                _difficultyState.SetDifficulty(newLevel);

            ResetFactories();
            ResetEntity();
            StartCoroutine(BeginRoundRoutine());
        }

        protected override void ExtendInitialize()
        {
            _waitingPoint.Reset();
        }

        private IEnumerator BeginRoundRoutine()
        {
            ResetFactories();
            _vesselFactory.Spawn();

            yield return new WaitUntil(() => _vesselFactory.IsReady);

            if (_vesselFactory.Objects != null &&
                _vesselFactory.Objects.Count > 0)
            {
                _columnsFactory.Initialize(_vesselFactory.Objects);
                _columnsFactory.Spawn();
            }
        }

        private void ResetEntity()
        {
            _wallet.Reset();
            _waitingPoint.Reset();
            _clickImpactHandler.Reset();
        }

        private void ResetFactories()
        {
            _vesselFactory.ResetFactory(
                _difficultyState.CurrentDifficulty);
            _columnsFactory.ResetFactory(
                _difficultyState.CurrentDifficulty);
        }

        private void ValidateObjects()
        {
            if (_columnsFactory == null)
                throw new ArgumentNullException(nameof(_columnsFactory));

            if (_vesselFactory == null)
                throw new ArgumentNullException(nameof(_vesselFactory));

            if (_waitingPoint == null)
                throw new ArgumentNullException(nameof(_waitingPoint));
        }
    }
}
using System.Collections;
using UnityEngine;
using GameDifficulty;
using FactoryCore;
using InteractiveObjects;
using System;

namespace GameBehaviour
{
    public class GameSessionHandler : BaseGameHandler
    {
        [SerializeField] private ColumnsFactory _columnsFactory;
        [SerializeField] private VesselFactory _vesselFactory;
        [SerializeField] private WaitingPoint _waitingPoint;

        private void Start()
        {
            ValidateObjects();
        }

        public void BeginNewRound()
        {
            ContinueGame();
            _wallet.Reset();
            _waitingPoint.Reset();
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
            _wallet.Reset();
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

        private void ResetFactories()
        {
            _vesselFactory.ResetFactory(_difficultyState.CurrentDifficulty);
            _columnsFactory.ResetFactory(_difficultyState.CurrentDifficulty);
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
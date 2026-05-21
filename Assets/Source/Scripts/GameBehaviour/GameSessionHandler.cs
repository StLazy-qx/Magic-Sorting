using Assets.Source.Scripts.EntryPoint;
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
        [SerializeField] private ClickModeSwitcher _clickImpactHandler;

        private int _currentRound = 1;
        private SequenceDifficultyLevel _sequenceDifficultyLevel;

        public event Action GameReseting;
        public event Action<int> RoundChanged;

        //зачем?
        public ClickModeSwitcher ClickImpactHandler => _clickImpactHandler;

        private void Awake()
        {
            _sequenceDifficultyLevel = new SequenceDifficultyLevel();
        }

        private void Start()
        {
            ValidateObjects();
            RoundChanged?.Invoke(_currentRound);
        }

        public void BeginNewRound()
        {
            //убрать повторяемость в коде
            ChangeDifficultyBySequence();
            ContinueGame();
            ResetEntity();
            StartCoroutine(BeginRoundRoutine());

            _currentRound++;
            RoundChanged?.Invoke(_currentRound);
        }

        public void IncreaseDifficultyLevel()
        {
            ContinueGame();

            DifficultyLevel current = DifficultyState.CurrentDifficulty;
            DifficultyLevel newLevel = GetIncreasedDifficulty(current);

            if (newLevel != current)
                DifficultyState.SetDifficulty(newLevel);

            ResetFactories();
            ResetEntity();
            StartCoroutine(BeginRoundRoutine());
        }

        protected override void ExtendInitialize()
        {
            _waitingPoint.Reset();
        }

        private void ChangeDifficultyBySequence()
        {
            DifficultyLevel nextLevel = _sequenceDifficultyLevel.GetNext();

            DifficultyState.SetDifficulty(nextLevel);
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

            GameReseting?.Invoke();
        }

        private void ResetEntity()
        {
            Wallet.Reset();
            _waitingPoint.Reset();
            _clickImpactHandler.Reset();
        }

        private void ResetFactories()
        {
            _vesselFactory.ResetFactory(
                DifficultyState.CurrentDifficulty);
            _columnsFactory.ResetFactory(
                DifficultyState.CurrentDifficulty);
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
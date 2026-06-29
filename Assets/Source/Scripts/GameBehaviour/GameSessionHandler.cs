using Assets.Source.Scripts.EntryPoint;
using Assets.Source.Scripts.Factory;
using Assets.Source.Scripts.Enums;
using Assets.Source.Scripts.InteractiveObjects;
using Assets.Source.Scripts.ActionsHandlers;
using Assets.Source.Scripts.GameDifficulty;
using Assets.Source.Scripts.Colorize;
using System.Collections;
using UnityEngine;
using System;
using Zenject;
using Assets.Source.Scripts.Extensions;

namespace Assets.Source.Scripts.GameBehaviour
{
    public class GameSessionHandler : BaseGameHandler
    {
        [SerializeField] private ColumnsFactory _columnsFactory;
        [SerializeField] private VesselFactory _vesselFactory;
        [SerializeField] private EntryColorListsFactory _entryColorListsFactory;
        [SerializeField] private ColorRandomizer _colorRandomizer;
        [SerializeField] private WaitingPoint _waitingPoint;
        [SerializeField] private ClickModeSwitcher _clickImpactHandler;
        [SerializeField] private ColorColumnDistributor _columnDistributor;
        [SerializeField] private DifficultyDatabase _difficultyDatabase;

        private SequenceDifficultyLevel _sequenceDifficultyLevel;
        private DifficultyState _difficultyState;
        private DifficultySettings _currentSettings;

        public event Action GameReseting;
        public event Action<int> RoundChanged;

        //зачем?
        public ClickModeSwitcher ClickImpactHandler => _clickImpactHandler;

        private void Awake()
        {
            _sequenceDifficultyLevel = new SequenceDifficultyLevel();

            ValidateObjects();
        }

        [Inject]
        private void Construct(DifficultyState difficultyState)
        {
            _difficultyState = difficultyState;
        }

        public void BeginNewRound()
        {
            ChangeDifficultyBySequence();

            _currentSettings = _difficultyDatabase.GetSettings(DifficultyState.CurrentDifficulty);
            _colorRandomizer.CrateArrayColors(_currentSettings.ColorsCount);

            StartRound();
        }

        protected override void ExtendInitialize()
        {
            _waitingPoint.Reset();
        }

        private void StartRound()
        {
            ContinueGame();
            ResetEntity();
            StartCoroutine(BeginRoundRoutine());
        }

        private void ChangeDifficultyBySequence()
        {
            DifficultyLevel nextLevel = _sequenceDifficultyLevel.GetNext();

            DifficultyState.SetDifficulty(nextLevel);
        }

        private IEnumerator BeginRoundRoutine()
        {
            ResetFactories();
            _vesselFactory.InitRandomizer(_colorRandomizer);
            _entryColorListsFactory.Initialize(
                _colorRandomizer.BeginColors,
                _colorRandomizer.RemainingColors);
            _vesselFactory.Spawn();

            yield return new WaitUntil(() => _vesselFactory.IsReady);

            if (_vesselFactory.Objects != null && _vesselFactory.Objects.Count > 0)
            {
                _columnsFactory.Initialize(
                    _vesselFactory.Objects,
                    _currentSettings.ColumnsCount,
                    _currentSettings.MaxCellsPerColumn);
                _columnsFactory.Spawn();
            }

            _columnDistributor.Distribute();
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
            _entryColorListsFactory.Reset();
            _vesselFactory.ResetFactory(DifficultyState.CurrentDifficulty);
            _columnsFactory.ResetFactory(DifficultyState.CurrentDifficulty);
        }

        //public void BeginNewRound()
        //{
        //    //убрать повторяемость в коде
        //    ChangeDifficultyBySequence();

        //    _currentSettings = _difficultyDatabase.
        //        GetSettings(DifficultyState.CurrentDifficulty);

        //    ContinueGame();
        //    _colorRandomizer.CrateArrayColors(_currentSettings.ColorsCount);
        //    ResetEntity();
        //    StartCoroutine(BeginRoundRoutine());
        //}

        //public void IncreaseDifficultyLevel()
        //{
        //    ContinueGame();

        //    DifficultyLevel current = DifficultyState.CurrentDifficulty;
        //    DifficultyLevel newLevel = GetIncreasedDifficulty(current);

        //    if (newLevel != current)
        //        DifficultyState.SetDifficulty(newLevel);

        //    ResetFactories();
        //    ResetEntity();
        //    StartCoroutine(BeginRoundRoutine());
        //}

        //protected override void ExtendInitialize()
        //{
        //    _waitingPoint.Reset();
        //}

        //private void ChangeDifficultyBySequence()
        //{
        //    DifficultyLevel nextLevel = _sequenceDifficultyLevel.GetNext();

        //    DifficultyState.SetDifficulty(nextLevel);
        //}

        //private IEnumerator BeginRoundRoutine()
        //{
        //    ResetFactories();
        //    _vesselFactory.InitRandomizer(_colorRandomizer);
        //    _entryColorListsFactory.Initialize(_colorRandomizer.Colors);
        //    _vesselFactory.Spawn();

        //    yield return new WaitUntil(() => _vesselFactory.IsReady);

        //    if (_vesselFactory.Objects != null &&
        //        _vesselFactory.Objects.Count > 0)
        //    {
        //        _columnsFactory.Initialize(
        //            _vesselFactory.Objects,
        //            _currentSettings.ColumnsCount,
        //            _currentSettings.MaxCellsPerColumn);
        //        _columnsFactory.Spawn();
        //    }

        //    _columnDistributor.Distribute();
        //    GameReseting?.Invoke();
        //}

        //private void ResetEntity()
        //{
        //    Wallet.Reset();
        //    _waitingPoint.Reset();
        //    _clickImpactHandler.Reset();
        //}

        //private void ResetFactories()
        //{
        //    _entryColorListsFactory.Reset();
        //    _vesselFactory.ResetFactory(
        //        DifficultyState.CurrentDifficulty);
        //    _columnsFactory.ResetFactory(
        //        DifficultyState.CurrentDifficulty);
        //}

        private void ValidateObjects()
        {
            Guard.NotNull(_columnsFactory, nameof(_columnsFactory));
            Guard.NotNull(_vesselFactory, nameof(_vesselFactory));
            Guard.NotNull(_entryColorListsFactory, nameof(_entryColorListsFactory));
            Guard.NotNull(_colorRandomizer, nameof(_colorRandomizer));
            Guard.NotNull(_waitingPoint, nameof(_waitingPoint));
            Guard.NotNull(_clickImpactHandler, nameof(_clickImpactHandler));
            Guard.NotNull(_columnDistributor, nameof(_columnDistributor));
            Guard.NotNull(_difficultyDatabase, nameof(_difficultyDatabase));
        }
    }
}
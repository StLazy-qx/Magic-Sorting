using System.Collections;
using UnityEngine;
using Zenject;
using GameDifficulty;
using FactoryCore;
using InteractiveObjects;
using PlayerCore;

namespace GameBehaviour
{
    public class GameSessionHandler : BaseGameHandler
    {
        [SerializeField] private ColumnsFactory _columnsFactory;
        [SerializeField] private VesselFactory _vesselFactory;
        [SerializeField] private WaitingPoint _waitingPoint;

        public void BeginNewRound()
        {
            ContinueGame();
            _wallet.Reset();
            _waitingPoint.Reset();
            StartCoroutine(StartNewRoundRoutine());
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
            StartCoroutine(StartNewRoundRoutine());
        }

        protected override void ExtendInitialize()
        {
            _waitingPoint.Reset();
        }

        [Inject]
        private void Construct(Wallet wallet, DifficultyState difficultyState)
        {
            _wallet = wallet;
            _difficultyState = difficultyState;
        }

        private IEnumerator StartNewRoundRoutine()
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
    }
}
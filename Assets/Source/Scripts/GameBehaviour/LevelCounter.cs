using Assets.Source.Scripts.EntryPoint;
using Assets.Source.Scripts.Extensions;
using System;
using UnityEngine;

namespace Assets.Source.Scripts.GameBehaviour
{
    public class LevelCounter : MonoBehaviour
    {
        private SequenceDifficultyLevel _currentLevel;

        public event Action<int> RoundChanged;

        public int RoundNumber => _currentLevel.RoundNumber;

        public void Initialize(SequenceDifficultyLevel level)
        {
            Guard.NotNull(level, nameof(level));

            _currentLevel = level;
        }

        private void OnEnable()
        {
            _currentLevel.RoundChanged += OnRoundChange;
        }

        private void OnDisable()
        {
            _currentLevel.RoundChanged -= OnRoundChange;
        }

        private void OnRoundChange(int roundNumber)
        {
            Guard.NotNull(roundNumber, nameof(roundNumber));
            RoundChanged?.Invoke(roundNumber);
        }
    }
}

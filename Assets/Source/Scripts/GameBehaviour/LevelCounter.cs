using Assets.Source.Scripts.EntryPoint;
using Assets.Source.Scripts.Extensions;
using System;
using UnityEngine;

namespace Assets.Source.Scripts.GameBehaviour
{
    public class LevelCounter : MonoBehaviour
    {
        private SequenceDifficultyLevel _currentLevel;
        private int _currentRound = 1;

        public event Action<int> RoundChanged;

        public int RoundNumber => _currentRound;

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

        private void OnRoundChange(int value)
        {
            _currentRound = value;

            RoundChanged?.Invoke(_currentRound);
        }
    }
}

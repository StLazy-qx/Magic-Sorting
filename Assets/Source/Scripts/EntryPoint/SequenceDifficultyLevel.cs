using Assets.Source.Scripts.Enums;
using System.Collections.Generic;
using System;
using YG;

namespace Assets.Source.Scripts.EntryPoint
{
    public class SequenceDifficultyLevel : IObjectInitilizable
    {
        private int _currentIndex;
        private int _amountRounds = 55;
        private List<DifficultyLevel> _sequence = new();

        public event Action<int> RoundChanged;

        public int RoundNumber => _currentIndex + 1;
        public bool IsInitialized { get; private set; }

        public void Initialize()
        {
            _sequence = new List<DifficultyLevel>(_amountRounds);

            InitSequence();
            LoadRound();

            IsInitialized = true;
        }

        public DifficultyLevel GetNext()
        {
            DifficultyLevel level = _sequence[_currentIndex];
            _currentIndex++;

            SaveRound();

            return level;
        }

        private void LoadRound()
        {
            int roundNumber = YG2.saves.GetRoundNumber();
            _currentIndex = roundNumber - 1;

            RoundChanged?.Invoke(roundNumber);
        }

        private void SaveRound()
        {
            int roundNumber = _currentIndex + 1;

            YG2.saves.SaveRoundNumber(roundNumber);
            RoundChanged?.Invoke(roundNumber);
        }

        private void InitSequence()
        {
            DifficultyLevel[] sequence = new DifficultyLevel[]
                {
                DifficultyLevel.Easy,
                DifficultyLevel.MediumEasy,
                DifficultyLevel.MediumEasy,
                DifficultyLevel.Medium,
                DifficultyLevel.MediumEasy,
                DifficultyLevel.Easy,
                DifficultyLevel.Medium,
                DifficultyLevel.MediumEasy,
                DifficultyLevel.Medium,
                DifficultyLevel.MediumHard,
                DifficultyLevel.Medium,
                DifficultyLevel.MediumEasy,
                DifficultyLevel.Easy,
                DifficultyLevel.MediumEasy,
                DifficultyLevel.Medium,
                DifficultyLevel.MediumHard,
                DifficultyLevel.Hard,
                DifficultyLevel.MediumHard,
                DifficultyLevel.Medium,
                DifficultyLevel.MediumEasy,
                DifficultyLevel.Easy,
                DifficultyLevel.MediumEasy,
                DifficultyLevel.Medium,
                DifficultyLevel.MediumHard,
                DifficultyLevel.Hard,
                DifficultyLevel.MediumHard,
                DifficultyLevel.Medium,
                DifficultyLevel.MediumEasy,
                DifficultyLevel.Easy,
                DifficultyLevel.MediumEasy,
                DifficultyLevel.Medium,
                DifficultyLevel.MediumHard,
                DifficultyLevel.Hard,
                DifficultyLevel.MediumHard,
                DifficultyLevel.Medium,
                DifficultyLevel.Hard,
                DifficultyLevel.Medium,
                DifficultyLevel.MediumEasy,
                DifficultyLevel.MediumHard,
                DifficultyLevel.Hard,
                DifficultyLevel.Hard,
                DifficultyLevel.MediumHard,
                DifficultyLevel.Medium,
                DifficultyLevel.MediumEasy,
                DifficultyLevel.Easy,
                DifficultyLevel.MediumEasy,
                DifficultyLevel.Medium,
                DifficultyLevel.MediumHard,
                DifficultyLevel.Hard,
                DifficultyLevel.MediumHard,
                DifficultyLevel.Medium,
                DifficultyLevel.Hard,
                DifficultyLevel.Medium,
                DifficultyLevel.MediumEasy,
                DifficultyLevel.MediumHard,
                DifficultyLevel.Hard
                };

            _sequence.AddRange(sequence);
        }
    }
}

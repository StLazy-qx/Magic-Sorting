using Assets.Source.Scripts.Enums;
using System.Collections.Generic;
using System;
using YG;
using UnityEngine;

namespace Assets.Source.Scripts.EntryPoint
{
    public class SequenceDifficultyLevel
    {
        private readonly List<DifficultyLevel> _sequence;

        private int _currentIndex;
        private int _amountRounds = 40;

        public event Action<int> RoundChanged;

        public int RoundNumber => _currentIndex + 1;

        public SequenceDifficultyLevel()
        {
            _sequence = new List<DifficultyLevel>(_amountRounds);

            InitSequence();
            LoadRound();
        }

        public DifficultyLevel GetNext()
        {
            if (_currentIndex >= _sequence.Count)
            {
                throw new InvalidOperationException(
                    "Нет доступных элементов для GetNext.");
            }

            DifficultyLevel level = _sequence[_currentIndex];
            _currentIndex++;

            SaveRound();

            return level;
        }

        private void LoadRound()
        {
            int roundNumber = YG2.saves.GetRoundNumber();

            Debug.Log("Загруженный уровень - " + roundNumber);

            _currentIndex = roundNumber - 1;

            RoundChanged?.Invoke(roundNumber);
        }

        private void SaveRound()
        {
            int roundNumber = _currentIndex + 1;

            RoundChanged?.Invoke(roundNumber);
            YG2.saves.SaveRoundNumber(roundNumber);
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
                DifficultyLevel.Hard
                };

            _sequence.AddRange(sequence);
        }
    }
}

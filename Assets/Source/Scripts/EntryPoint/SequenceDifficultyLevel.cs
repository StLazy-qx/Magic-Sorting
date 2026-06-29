using Assets.Source.Scripts.Enums;
using System.Collections.Generic;
using System;

namespace Assets.Source.Scripts.EntryPoint
{
    public class SequenceDifficultyLevel
    {
        private List<DifficultyLevel> _sequence;
        private int _currentIndex;
        private int _amountRounds = 40;

        public SequenceDifficultyLevel()
        {
            _sequence = new List<DifficultyLevel>(_amountRounds);

            InitSequence();

            _currentIndex = 0;
        }

        public DifficultyLevel GetCurrent()
        {
            if (_currentIndex >= _sequence.Count)
            {
                throw new InvalidOperationException(
                    "Последовательность исчерпана. Текущий элемент недоступен.");
            }

            return _sequence[_currentIndex];
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

            return level;
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

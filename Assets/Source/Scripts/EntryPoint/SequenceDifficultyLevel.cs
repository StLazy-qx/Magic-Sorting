using Assets.Source.Scripts.Enums;
using System.Collections.Generic;
using System;
using YG;

namespace Assets.Source.Scripts.EntryPoint
{
    public class SequenceDifficultyLevel : IObjectInitilizable
    {
        private const int InitialSequenceLength = 50;
        private const int ExtendBatchSize = 50;
        private const float EasyProbability = 40f;
        private const float MediumEasyProbability = 25f;
        private const float MediumProbability = 20f;
        private const float MediumHardProbability = 10f;

        private int _currentIndex;
        private List<DifficultyLevel> _sequence = new();

        public event Action<int> RoundChanged;

        public int RoundNumber => _currentIndex + 1;
        public bool IsInitialized { get; private set; }

        public void Initialize()
        {
            _sequence = new List<DifficultyLevel>(InitialSequenceLength);

            ExtendSequence(InitialSequenceLength);
            //InitSequence();
            LoadRound();

            IsInitialized = true;
        }

        public DifficultyLevel GetNext()
        {
            while (_currentIndex >= _sequence.Count)
            {
                ExtendSequence(ExtendBatchSize);
            }

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

        private void ExtendSequence(int count)
        {
            for (int i = 0; i < count; i++)
            {
                _sequence.Add(GetRandomDifficulty());
            }
        }

        private DifficultyLevel GetRandomDifficulty()
        {
            float randomValue = UnityEngine.Random.Range(0f, 100f);

            if (randomValue < EasyProbability)
            {
                return DifficultyLevel.Easy;
            }

            if (randomValue < EasyProbability + MediumEasyProbability)
            {
                return DifficultyLevel.MediumEasy;
            }

            if (randomValue < EasyProbability +
                MediumEasyProbability + MediumProbability)
            {
                return DifficultyLevel.Medium;
            }

            if (randomValue < EasyProbability +
                MediumEasyProbability + MediumProbability +
                MediumHardProbability)
            {
                return DifficultyLevel.MediumHard;
            }

            return DifficultyLevel.Hard;
        }

        //private void InitSequence()
        //{
        //    DifficultyLevel[] sequence = new DifficultyLevel[]
        //        {
        //        DifficultyLevel.Easy,
        //        DifficultyLevel.MediumEasy,
        //        DifficultyLevel.MediumEasy,
        //        DifficultyLevel.Medium,
        //        DifficultyLevel.MediumEasy,
        //        DifficultyLevel.Easy,
        //        DifficultyLevel.Medium,
        //        DifficultyLevel.MediumEasy,
        //        DifficultyLevel.Medium,
        //        DifficultyLevel.MediumHard,
        //        DifficultyLevel.Medium,
        //        DifficultyLevel.MediumEasy,
        //        DifficultyLevel.Easy,
        //        DifficultyLevel.MediumEasy,
        //        DifficultyLevel.Medium,
        //        DifficultyLevel.MediumHard,
        //        DifficultyLevel.Hard,
        //        DifficultyLevel.MediumHard,
        //        DifficultyLevel.Medium,
        //        DifficultyLevel.MediumEasy,
        //        DifficultyLevel.Easy,
        //        DifficultyLevel.MediumEasy,
        //        DifficultyLevel.Medium,
        //        DifficultyLevel.MediumHard,
        //        DifficultyLevel.Hard,
        //        DifficultyLevel.MediumHard,
        //        DifficultyLevel.Medium,
        //        DifficultyLevel.MediumEasy,
        //        DifficultyLevel.Easy,
        //        DifficultyLevel.MediumEasy,
        //        DifficultyLevel.Medium,
        //        DifficultyLevel.MediumHard,
        //        DifficultyLevel.Hard,
        //        DifficultyLevel.MediumHard,
        //        DifficultyLevel.Medium,
        //        DifficultyLevel.Hard,
        //        DifficultyLevel.Medium,
        //        DifficultyLevel.MediumEasy,
        //        DifficultyLevel.MediumHard,
        //        DifficultyLevel.Hard,
        //        DifficultyLevel.Hard,
        //        DifficultyLevel.MediumHard,
        //        DifficultyLevel.Medium,
        //        DifficultyLevel.MediumEasy,
        //        DifficultyLevel.Easy,
        //        DifficultyLevel.MediumEasy,
        //        DifficultyLevel.Medium,
        //        DifficultyLevel.MediumHard,
        //        DifficultyLevel.Hard,
        //        DifficultyLevel.MediumHard,
        //        DifficultyLevel.Medium,
        //        DifficultyLevel.Hard,
        //        DifficultyLevel.Medium,
        //        DifficultyLevel.MediumEasy,
        //        DifficultyLevel.MediumHard,
        //        DifficultyLevel.Hard
        //        };

        //    _sequence.AddRange(sequence);
        //}
    }
}

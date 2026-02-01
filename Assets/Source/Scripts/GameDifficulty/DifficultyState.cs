using Assets.Source.Scripts.Enums;
using System;
using Zenject;

namespace Assets.Source.Scripts.GameDifficulty
{
    public class DifficultyState : IInitializable
    {
        public DifficultyLevel CurrentDifficulty { get; private set; } = DifficultyLevel.Easy;

        public event Action<DifficultyLevel> DifficultyChanged;

        public void SetDifficulty(DifficultyLevel level)
        {
            if (CurrentDifficulty == level)
                return;

            CurrentDifficulty = level;

            DifficultyChanged?.Invoke(level);
        }

        public void Initialize() { }
    }
}
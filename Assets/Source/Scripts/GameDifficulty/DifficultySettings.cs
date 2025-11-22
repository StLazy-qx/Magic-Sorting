using System;
using UnityEngine;

namespace GameDifficulty
{
    [CreateAssetMenu(menuName = "Config/Difficulty Setting", 
        fileName = "Difficulty Setting", order = 51)]

    public class DifficultySettings : ScriptableObject
    {
        public DifficultyLevel level;

        [Header("Columns settings")]
        public int maxSpawnPoints;
        public int minCellsPerColumn;

        [Header("Vessels settings")]
        public int vesselsCount;
        public int colorsCount;

        private void OnValidate()
        {
            ValidatePositive(maxSpawnPoints, nameof(maxSpawnPoints));
            ValidatePositive(minCellsPerColumn, nameof(minCellsPerColumn));
            ValidatePositive(vesselsCount, nameof(vesselsCount));
            ValidatePositive(colorsCount, nameof(colorsCount));
        }

        private void ValidatePositive(int value, string paramName)
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException(paramName,
                    $"{paramName} должен быть > 0");
            }
        }
    }
}
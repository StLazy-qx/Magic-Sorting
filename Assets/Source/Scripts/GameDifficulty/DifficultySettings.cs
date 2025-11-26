using System;
using UnityEngine;

namespace Assets.Source.Scripts.GameDifficulty
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
            ValidateValue(maxSpawnPoints, nameof(maxSpawnPoints));
            ValidateValue(minCellsPerColumn, nameof(minCellsPerColumn));
            ValidateValue(vesselsCount, nameof(vesselsCount));
            ValidateValue(colorsCount, nameof(colorsCount));
        }

        private void ValidateValue(int value, string paramName)
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException(paramName,
                    $"{paramName} должен быть > 0");
            }
        }
    }
}
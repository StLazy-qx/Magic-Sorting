using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameDifficulty
{
    [CreateAssetMenu(menuName = "Config/Difficulty Database", 
        fileName = "DifficultyDatabase", order = 51)]

    public class DifficultyDatabase : ScriptableObject
    {
        public DifficultySettings[] _parameters;

        public DifficultySettings GetSettings(DifficultyLevel level)
        {
            if (_parameters == null || _parameters.Length == 0)
                throw new InvalidOperationException("DifficultyDatabase: no parameters.");

            foreach (DifficultySettings parameter in _parameters)
            {
                if (parameter.level == level)
                    return parameter;
            }

            throw new KeyNotFoundException(
                $"DifficultyDatabase: no settings for the level {level}");
        }
    }
}
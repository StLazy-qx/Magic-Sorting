using UnityEngine;

[CreateAssetMenu(menuName = "Config/Difficulty Database", fileName = "DifficultyDatabase", order = 51)]
public class DifficultyDatabase : ScriptableObject
{
    public DifficultySettings[] _parameters;

    public DifficultySettings GetSettings(DifficultyLevel level)
    {
        foreach (DifficultySettings parameter in _parameters)
        {
            if (parameter.level == level)
                return parameter;
        }

        return null;
    }
}

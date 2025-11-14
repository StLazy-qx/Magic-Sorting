using UnityEngine;

[CreateAssetMenu(menuName = "Config/Difficulty Setting", fileName = "Difficulty Setting", order = 51)]
public class DifficultySettings : ScriptableObject
{
    public DifficultyLevel level;

    [Header("Columns settings")]
    public int maxSpawnPoints;
    public int minCellsPerColumn;

    [Header("Vessels settings")]
    public int vesselsCount;
    public int colorsCount;
}

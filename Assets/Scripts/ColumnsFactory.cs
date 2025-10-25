using System.Collections.Generic;
using UnityEngine;

public class ColumnsFactory : Factory<MagicColumn>
{
    [SerializeField] private MagicCellRouter _distributerMagicCell;
    [SerializeField] private ColumnColorDistributor _colorDistributor;

    //private IReadOnlyList<Vessel> _vessels;

    public void Initialize(IReadOnlyList<Vessel> vessels)
    {
        //_vessels = vessels;

        _colorDistributor.Initialize(vessels);
        _distributerMagicCell.Initialize(vessels);

        BuildObjects();
    }

    protected override void BuildObjects()
    {
        ClearList();

        int countSpawnPoints = CalculateSpawnPointsToUse();
        int cellsPerColumn = Mathf.Max(1,
            _colorDistributor.TotalColors / countSpawnPoints);

        for (int i = 0; i < countSpawnPoints; i++)
        {
            Transform point = SpawnPoints[i];
            MagicColumn columnInstance = Instantiate(Prefab,
                point.position,point.rotation);

            columnInstance.Initialize(
                _distributerMagicCell,
                _colorDistributor,
                cellsPerColumn);

            Add(columnInstance);
        }

        NotifyObjectsChanged();
    }

    //или название метода через get?
    private int CalculateSpawnPointsToUse()
    {
        if (CurrentSettings == null && DifficultyDatabase != null)
        {
            CurrentSettings = DifficultyDatabase.GetSettings(DifficultyState.CurrentDifficulty);
        }

        return Mathf.Min(CurrentSettings.maxSpawnPoints, SpawnPoints.Length);
    }
}

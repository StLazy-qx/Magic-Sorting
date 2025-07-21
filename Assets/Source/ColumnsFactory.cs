using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnsFactory : MonoBehaviour
{
    [Header("Prefabs & References")]
    [SerializeField] private MagicColumn _columnPrefab;
    [SerializeField] private MagicCell _magicCellPrefab;
    [SerializeField] private DistributerMagicCell _distributerMagicCell;
    [SerializeField] private BuildMagicColumn _buildMagicColumn;

    [Header("Spawn Settings")]
    [SerializeField] private Transform[] _spawnPoints;

    private MagicColumn[] _spawnedColumns;

    private void Start()
    {
        BuildColumns();
    }

    public void BuildColumns()
    {
        if (_columnPrefab == null)
        {
            Debug.LogError("[ColumnsFactory] Column prefab не назначен.");

            return;
        }

        if (_buildMagicColumn == null)
        {
            Debug.LogError("[ColumnsFactory] BuildMagicColumn не назначен.");

            return;
        }

        if (_spawnPoints == null || _spawnPoints.Length == 0)
        {
            Debug.LogError("[ColumnsFactory] Нет точек спавна.");

            return;
        }

        if (_buildMagicColumn.IsInitialized == false)
        {
            Debug.LogError("[ColumnsFactory] " +
                "BuildMagicColumn не инициализирован!");

            return;
        }

        int totalColors = _buildMagicColumn.TotalColors;
        int numberOfPoints = _spawnPoints.Length;

        if (totalColors == 0)
        {
            Debug.LogError("[ColumnsFactory] " +
                "Нет доступных цветов в BuildMagicColumn.");

            return;
        }

        if (totalColors < numberOfPoints)
        {
            Debug.LogWarning("[ColumnsFactory] Цветов меньше, " +
                "чем столбов. Не все столбы будут заполнены.");
        }

        int cellsPerColumn = totalColors / numberOfPoints;

        if (cellsPerColumn == 0)
            cellsPerColumn = 1; // fallback, чтобы не было деления на 0

        _spawnedColumns = new MagicColumn[numberOfPoints];

        for (int i = 0; i < numberOfPoints; i++)
        {
            Transform point = _spawnPoints[i];

            if (point == null)
            {
                Debug.LogWarning($"[ColumnsFactory] SpawnPoint {i} == null, пропуск.");

                continue;
            }

            MagicColumn columnInstance = Instantiate(_columnPrefab, point.position, point.rotation, point);
            columnInstance.name = $"MagicColumn_{i}";

            columnInstance.Initialize(_magicCellPrefab, _distributerMagicCell, _buildMagicColumn, cellsPerColumn);

            _spawnedColumns[i] = columnInstance;
        }
    }
}

using System.Collections.Generic;
using UnityEngine;
using Colorize;
using InteractiveObjects;
using MagicCells;
using Vessels;
using System;
using System.Linq;

namespace FactoryCore
{
    public class ColumnsFactory : Factory<MagicColumn>
    {
        [SerializeField] private MagicCellRouter _distributerMagicCell;
        [SerializeField] private ShuffledColorDistributor _colorDistributor;

        public void Initialize(IReadOnlyList<Vessel> vessels)
        {
            if (_distributerMagicCell == null)
                throw new ArgumentNullException(nameof(_distributerMagicCell));

            if (_colorDistributor == null)
                throw new ArgumentNullException(nameof(_colorDistributor));

            ValidateVessels(vessels);
            _colorDistributor.Initialize(vessels);
            _distributerMagicCell.Initialize(vessels);
        }

        protected override void BuildObjects()
        {
            ClearList();

            int countSpawnPoints = CalculateSpawnPoints();
            int cellsPerColumn = Mathf.Max(1,
                _colorDistributor.TotalColors / countSpawnPoints);

            for (int i = 0; i < countSpawnPoints; i++)
            {
                Transform point = SpawnPoints[i];
                MagicColumn columnInstance = Instantiate(Prefab,
                    point.position, point.rotation);

                columnInstance.Initialize(
                    _distributerMagicCell,
                    _colorDistributor,
                    cellsPerColumn);

                Add(columnInstance);
            }

            NotifyObjectsChanged();
        }

        private int CalculateSpawnPoints()
        {
            if (CurrentSettings == null && DifficultyDatabase != null)
            {
                CurrentSettings = DifficultyDatabase.
                    GetSettings(DifficultyState.CurrentDifficulty);
            }

            return Mathf.Min(CurrentSettings.maxSpawnPoints, SpawnPoints.Length);
        }

        private void ValidateVessels(IReadOnlyList<Vessel> vessels)
        {
            if (vessels == null)
                throw new ArgumentNullException(nameof(vessels), "The list of vessels must be initialized");

            if (vessels.Count == 0)
                throw new ArgumentException("The list of vessels cannot be empty", nameof(vessels));

            if (vessels.Any(vessel => vessel == null))
                throw new ArgumentException("The vessel list contains a zero element", nameof(vessels));
        }
    }
}
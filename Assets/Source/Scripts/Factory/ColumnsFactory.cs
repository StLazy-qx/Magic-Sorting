using System.Collections.Generic;
using Assets.Source.Scripts.Colorize;
using Assets.Source.Scripts.InteractiveObjects;
using Assets.Source.Scripts.MagicCells;
using Assets.Source.Scripts.Vessels;
using UnityEngine;
using System;
using System.Linq;
using Assets.Source.Scripts.Pool;

namespace Assets.Source.Scripts.Factory
{
    public class ColumnsFactory : Factory<MagicColumn>
    {
        [SerializeField] private MagicColumnPool _columnPool;
        [SerializeField] private MagicCellRouter _distributerMagicCell;
        [SerializeField] private ShuffledColorDistributor _colorDistributor;

        public void Initialize(IReadOnlyList<MonoVessel> vessels)
        {
            if (_distributerMagicCell == null)
                throw new ArgumentNullException(nameof(_distributerMagicCell));

            if (_colorDistributor == null)
                throw new ArgumentNullException(nameof(_colorDistributor));

            if (_columnPool == null)
                throw new ArgumentNullException(nameof(_columnPool));

            ValidateVessels(vessels);
            _colorDistributor.Initialize(vessels);
            _distributerMagicCell.Initialize(vessels);
        }

        protected override void BuildObjects()
        {
            if (_columnPool == null)
                throw new ArgumentNullException(nameof(_columnPool));

            _columnPool.DeactivateAll();

            int countSpawnPoints = CalculateSpawnPoints();
            int cellsPerColumn = Mathf.Max(1,
                _colorDistributor.TotalColors / countSpawnPoints);

            for (int i = 0; i < countSpawnPoints; i++)
            {
                Transform point = SpawnPoints[i];

                MagicColumn column = _columnPool.Activate();

                if (column == null)
                {
                    column = Instantiate(Prefab,
                        point.position,
                        point.rotation,
                        _columnPool.Container);

                    column.Initialize(
                        _distributerMagicCell,
                        _colorDistributor,
                        cellsPerColumn);

                    _columnPool.Add(column);
                    column = _columnPool.Activate();
                }

                column.transform.SetPositionAndRotation(
                    point.position,
                    point.rotation);
            }
        }

        private int CalculateSpawnPoints()
        {
            if (CurrentSettings == null && DifficultyDatabase != null)
            {
                CurrentSettings = DifficultyDatabase.
                    GetSettings(DifficultyState.CurrentDifficulty);
            }

            return Mathf.Min(CurrentSettings.columnsCount, SpawnPoints.Length);
        }

        private void ValidateVessels(IReadOnlyList<MonoVessel> vessels)
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
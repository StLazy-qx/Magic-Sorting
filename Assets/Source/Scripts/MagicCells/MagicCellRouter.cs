using System;
using System.Collections.Generic;
using UnityEngine;
using Assets.Source.Scripts.InteractiveObjects;
using Assets.Source.Scripts.Vessels;

namespace Assets.Source.Scripts.MagicCells
{
    public class MagicCellRouter : MonoBehaviour
    {
        [SerializeField] private WaitingPoint _waitingPoint;

        private IReadOnlyList<Vessel> _vessels;

        public event Action<Vector3, Vector3, Color> CellDelivering;

        public void Initialize(IReadOnlyList<Vessel> vessels)
        {
            if (vessels == null)
            {
                throw new ArgumentNullException(
                    nameof(vessels), "[DistributerMagicCell] " +
                    "Список судов не может быть пустым.");
            }

            if (vessels.Count == 0)
            {
                throw new ArgumentException(
                    "[MagicCellRouter] Список судов не может быть пустым.",
                    nameof(vessels));
            }

            _vessels = vessels;
        }

        public void DeliverMagicCell(MagicCell cell)
        {
            if (cell == null)
            {
                throw new ArgumentNullException(nameof(cell),
                    "[DistributerMagicCell] Волшебная ячейка не может быть нулевой.");
            }

            Color cellColor = cell.Color;
            Vessel targetVessel = FindVesselByColor(cellColor);

            if (targetVessel != null)
            {
                targetVessel.TakeMagic(cell);

                CellDelivering?.Invoke(
                    cell.transform.position,
                    targetVessel.transform.position,
                    cell.Color
                    );
            }
            else if (_waitingPoint.IsFreePlace)
            {
                _waitingPoint.AcceptStorageCell(cell);

                CellDelivering?.Invoke(
                    cell.transform.position,
                    _waitingPoint.transform.position,
                    cell.Color
                    );
            }
        }

        public bool IsCheckCellColor(Color color)
        {
            if (FindVesselByColor(color) != null)
                return true;

            return _waitingPoint.IsFreePlace;
        }

        private Vessel FindVesselByColor(Color color)
        {
            foreach (Vessel vessel in _vessels)
            {
                if (vessel.IsActive && vessel.Color == color)
                {
                    return vessel;
                }
            }

            return null;
        }
    }
}
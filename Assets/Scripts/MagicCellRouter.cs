using System;
using System.Collections.Generic;
using UnityEngine;

public class MagicCellRouter : MonoBehaviour
{
    [SerializeField] private WaitingPoint _waitingPoint;

    private IReadOnlyList<Vessel> _vessels;

    public event Action<Vector3, Vector3, Color> CellDelivering;
    //public bool IsReady { get; private set; }

    public void DeliverMagicCell(MagicCell cell)
    {
        if (cell == null)
            throw new ArgumentNullException(nameof(cell),
                "[DistributerMagicCell] Волшебная ячейка не может быть нулевой.");

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
        else if (_waitingPoint.IsSeat)
        {
            _waitingPoint.HoldOverflowCell(cell);

            CellDelivering?.Invoke(
                cell.transform.position, 
                targetVessel.transform.position, 
                cell.Color
                );
        }
        else
        {
            Debug.LogWarning("Невозможно доставить ячейку - " +
                "нет подходящего сосуда и точка ожидания занята");
        }
    }

    public void Initialize(IReadOnlyList<Vessel> vessels)
    {
        if (vessels == null)
        {
            throw new ArgumentNullException(
                nameof(vessels),"[DistributerMagicCell] " +
                "Список судов не может быть пустым.");
        }

        _vessels = vessels;
    }

    public bool IsCheckCellColor(Color color)
    {
        if (FindVesselByColor(color) != null)
            return true;

        return _waitingPoint.IsSeat;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistributerMagicCell : MonoBehaviour
{
    private IReadOnlyList<Vessel> _vessels;

    //private void Awake()
    //{
    //    _vessels = new IReadOnlyList<Vessel>();
    //}

    //убрать дубляж

    public void DeliverMagicCell(MagicCell cell)
    {
        if (cell == null)
            return;

        Color cellColor = cell.Color;

        foreach (Vessel vessel in _vessels)
        {
            if (cellColor == vessel.Color)
            {
                vessel.TakeMagic(cell);
            }
        }
    }

    public void AcceptVesselsList(IReadOnlyList<Vessel> vessels)
    {
        //проверка

        _vessels = vessels;
    }

    public bool IsCheckCellColor(MagicCell cell)
    {
        if (cell == null)
            return false;

        Color cellColor = cell.Color;

        foreach (Vessel vessel in _vessels)
        {
            if (cellColor == vessel.Color)
            {
                return true;
            }
        }

        return false;
    }
}

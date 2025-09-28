using System;
using System.Collections.Generic;
using UnityEngine;

public class WaitingPoint : MonoBehaviour
{
    [SerializeField] private int _seatsNumber;
    [SerializeField] private Transform _storagePoint;
    //[SerializeField] private Transform[] _storagePoints;
    [SerializeField] private MagicCellRouter _cellRouter;

    private MagicCell _waitingCell;
    private ClickHandler _clickHandler;

    //private List<MagicCell> _waitingCells;
    //private List<ClickHandler> _clickHandlers;

    //public bool HasFreeSeat => _waitingCells.Count < _seatsNumber;

    public bool IsSeat { get; private set; }
    public MagicCell FreeCell => _waitingCell;

    private void Awake()
    {
        IsSeat = true;
        _waitingCell = null;

        //_waitingCells = new List<MagicCell>();
        //_clickHandlers = new List<ClickHandler>();
    }

    public void HoldOverflowCell(MagicCell cell)
    {
        //if (HasFreeSeat == false)
        //    return;

        //if (cell == null)
        //    throw new ArgumentNullException(nameof(cell), 
        //        "[WaitingPoint] Волшебная ячейка не может быть нулевой.");

        //MagicCell newCell = Instantiate(cell);
        //int index = _waitingCells.Count;

        //newCell.transform.position = _storagePoints[index].position;

        //var clickHandler = newCell.GetComponent<ClickHandler>();
        //clickHandler.OnClicked += () => OnCellClicked(newCell, clickHandler);

        //_waitingCells.Add(newCell);
        //_clickHandlers.Add(clickHandler);

        if (_waitingCell != null)
            return;

        if (cell == null)
            throw new ArgumentNullException(nameof(cell),
                "[WaitingPoint] Волшебная ячейка не может быть нулевой.");

        IsSeat = false;
        _waitingCell = Instantiate(cell);
        _waitingCell.transform.position = _storagePoint.position;

        _clickHandler = _waitingCell.GetComponent<ClickHandler>();
        _clickHandler.OnClicked += OnCellClicked;
    }

    //private void OnCellClicked(MagicCell cell, ClickHandler handler)
    //{
    //    if (_cellRouter.IsCheckCellColor(cell.Color) == false)
    //        return;

    //    _cellRouter.DeliverMagicCell(cell);

    //    handler.OnClicked -= () => OnCellClicked(cell, handler);

    //    _waitingCells.Remove(cell);
    //    _clickHandlers.Remove(handler);

    //    cell.Disable();
    //    Destroy(cell.gameObject);
    //}

    private void OnCellClicked()
    {
        MagicCell tempCell = _waitingCell;

        if (_cellRouter.IsCheckCellColor(tempCell.Color) == false)
            return;

        _cellRouter.DeliverMagicCell(tempCell);

        IsSeat = true;
        _waitingCell.Disable();
        _waitingCell = null;
        _clickHandler.OnClicked -= OnCellClicked;
    }
}

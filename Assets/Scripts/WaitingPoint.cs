using System;
using UnityEngine;

public class WaitingPoint : MonoBehaviour
{
    private readonly float RotationX = 90f;

    [SerializeField] private int _seatsNumber;
    [SerializeField] private Transform _storagePoint;
    [SerializeField] private MagicCellRouter _cellRouter;

    private MagicCell _waitingCell;
    private ClickHandler _clickHandler;

    public bool IsFreePlace { get; private set; }

    private void Awake()
    {
        Reset();
    }

    public void AcceptStorageCell(MagicCell cell)
    {
        if (_waitingCell != null && IsFreePlace == true)
            return;

        if (cell == null)
        {
            throw new ArgumentNullException(nameof(cell),
                "[WaitingPoint] Волшебная ячейка не может быть нулевой.");
        }

        IsFreePlace = false;
        Quaternion cellRotation = Quaternion.Euler(RotationX, 0f, 0f);
        _waitingCell = Instantiate(cell, _storagePoint.position, cellRotation);
        _clickHandler = _waitingCell.GetComponent<ClickHandler>();
        _clickHandler.OnClicked += OnCellClicked;
    }

    public void Reset()
    {
        IsFreePlace = true;

        if (_waitingCell != null)
        {
            Destroy(_waitingCell.gameObject);

            _waitingCell = null;
        }

        _clickHandler = null;
    }

    private void OnCellClicked()
    {
        MagicCell waitCell = _waitingCell;

        if (_cellRouter.IsCheckCellColor(waitCell.Color) == false)
            return;

        _cellRouter.DeliverMagicCell(waitCell);
        _waitingCell.Disable();
        Reset();

        _clickHandler.OnClicked -= OnCellClicked;
    }
}

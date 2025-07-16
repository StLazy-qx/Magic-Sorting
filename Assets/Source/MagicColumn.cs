using System;
using System.Collections.Generic;
using UnityEngine;

public class MagicColumn : MonoBehaviour
{
    [SerializeField] private int _countCells;
    [SerializeField] private MagicCell _cell;
    [SerializeField] private DistributerMagicCell _distributerMagic;

    private Stack<MagicCell> _cellsStack;
    private float _prefabHeight;
    private float _distanceBetweenCells = 0.05f;

    public event Action CellDisplacing;

    private void Awake()
    {
        _cellsStack = new();
        _prefabHeight = GetPrefabHeight();
    }

    void Start()
    {
        Spawn();
    }

    public void Spawn()
    {
        float currentY = transform.position.y;

        for (int i = 0; i < _countCells; i++)
        {
            MagicCell cell = Instantiate(_cell, transform);

            if (cell != null)
            {
                cell.transform.localPosition = new Vector3(0, currentY, 0);
                ClickHandler clickHandler = cell.GetComponent<ClickHandler>();

                if (clickHandler != null)
                {
                    clickHandler.OnClicked += OnCellClicked;
                }

                _cellsStack.Push(cell);

                currentY += _prefabHeight;
            }
        }
    }

    private void OnCellClicked()
    {
        if (_distributerMagic.IsCheckCellColor(_cellsStack.Peek()))
        {
            MagicCell topCell = _cellsStack.Pop();

            CellDisplacing?.Invoke();
            _distributerMagic.DeliverMagicCell(topCell);
            topCell.Disable();
        }
    }

    private float GetPrefabHeight()
    {
        Renderer renderer = _cell.GetComponentInChildren<Renderer>();

        return renderer.bounds.size.y + _distanceBetweenCells;
    }
}

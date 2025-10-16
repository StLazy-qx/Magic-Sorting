using System;
using System.Collections.Generic;
using UnityEngine;

public class MagicCellsStackHandler : MonoBehaviour
{
    private MagicCellsFactory _factory;
    private MagicCellRouter _cellRouter;
    private ColumnColorDistributor _colorSource;
    private Transform _parent;
    private float _prefabHeight;

    private Stack<MagicCell> _cellsStack = new();

    public void Initialize(
        MagicCellsFactory factory,
        MagicCellRouter cellRouter,
        ColumnColorDistributor colorSource,
        Transform parent,
        float prefabHeight)
    {
        ValidateArguments(factory, cellRouter, colorSource, parent);

        _factory = factory;
        _cellRouter = cellRouter;
        _colorSource = colorSource;
        _parent = parent;
        _prefabHeight = prefabHeight;
    }

    public void CreateCells(int countCells)
    {
        float currentY = 0f;

        for (int i = 0; i < countCells; i++)
        {
            if (_colorSource.TryGetRandomColor(out Color pickedColor) == false)
                return;

            MagicCell cell = _factory.CreateCell(
                parent: _parent,
                localPosition: new Vector3(0, currentY, 0),
                color: pickedColor);

            ClickHandler clickHandler = cell.GetComponent<ClickHandler>();

            if (clickHandler != null)
                clickHandler.OnClicked += OnCellClicked;

            _cellsStack.Push(cell);

            currentY += _prefabHeight;
        }
    }

    private void OnCellClicked()
    {
        if (_cellsStack.Count == 0)
            return;

        MagicCell topCell = _cellsStack.Peek();

        if (_cellRouter.IsCheckCellColor(topCell.Color) == false)
            return;

        MagicCell newTopCell = _cellsStack.Pop();

        _cellRouter.DeliverMagicCell(newTopCell);
        newTopCell.Disable();
    }

    private void ValidateArguments(
    MagicCellsFactory factory,
    MagicCellRouter cellRouter,
    ColumnColorDistributor colorSource,
    Transform parent)
    {
        if (factory == null)
            throw new ArgumentNullException(nameof(factory));

        if (cellRouter == null)
            throw new ArgumentNullException(nameof(cellRouter));

        if (colorSource == null)
            throw new ArgumentNullException(nameof(colorSource));

        if (parent == null)
            throw new ArgumentNullException(nameof(parent));
    }
}

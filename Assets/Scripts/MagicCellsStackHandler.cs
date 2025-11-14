using System;
using System.Collections.Generic;
using UnityEngine;

public class MagicCellsStackHandler : MonoBehaviour
{
    private MagicCellsFactory _factory;
    private MagicCellRouter _cellRouter;
    private ShuffledColorDistributor _colorSource;
    private Transform _parent;
    private float _prefabHeight;

    private Stack<MagicCell> _cellsStack = new();

    public void Initialize(
        MagicCellsFactory factory,
        MagicCellRouter cellRouter,
        ShuffledColorDistributor colorSource,
        Transform parent,
        int countCells,
        float prefabHeight)
    {
        ValidateArguments(factory, cellRouter, colorSource, parent);

        _factory = factory;
        _cellRouter = cellRouter;
        _colorSource = colorSource;
        _parent = parent;
        _prefabHeight = prefabHeight;

        CreateCells(countCells);
    }

    private void CreateCells(int countCells)
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

        if (_cellsStack.TryPop(out MagicCell cell) == false)
            return;

        if (_cellRouter.IsCheckCellColor(cell.Color) == false)
        {
            _cellsStack.Push(cell);

            return;
        }

        _cellRouter.DeliverMagicCell(cell);
        cell.Disable();
    }

    private void ValidateArguments(
    MagicCellsFactory factory,
    MagicCellRouter cellRouter,
    ShuffledColorDistributor colorSource,
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

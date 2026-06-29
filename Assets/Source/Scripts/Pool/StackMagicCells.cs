using Assets.Source.Scripts.ActionsHandlers;
using Assets.Source.Scripts.Colorize;
using Assets.Source.Scripts.Enums;
using Assets.Source.Scripts.Factory;
using Assets.Source.Scripts.GameBehaviour;
using Assets.Source.Scripts.InteractiveObjects;
using Assets.Source.Scripts.MagicCells;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Source.Scripts.Pool
{
    public class StackMagicCells : MonoBehaviour
    {
        [SerializeField] private GameSessionHandler _gameHandler;
        [SerializeField] private ColumnRevercer _columnRevercer;

        private MagicCellsFactory _factory;
        private MagicCellRouter _cellRouter;
        private ShuffledColorDistributor _colorSource;
        private EntryListColorPool _listColorPool;
        private ClickModeSwitcher _clickImpactHandler;
        private Transform _parent;
        private int _maxVolumeCells;
        private float _prefabHeight;
        private bool _isPaused;
        private Stack<MagicCell> _cellsStack = new();

        private void OnDisable()
        {
            _gameHandler.PauseStateChanged -= OnGamePause;
        }

        public void Initialize(
            MagicCellsFactory factory,
            MagicCellRouter cellRouter,
            ShuffledColorDistributor colorSource,
            EntryListColorPool listColorPool,
            ClickModeSwitcher clickHandler,
            Transform parent,
            int maxVolumeCells,
            float prefabHeight)
        {
            ValidateArguments(factory, cellRouter, colorSource, clickHandler, parent);
            ValidateValues(maxVolumeCells, prefabHeight);

            ClearStack();

            _factory = factory;
            _cellRouter = cellRouter;
            _colorSource = colorSource;
            _listColorPool = listColorPool;
            _clickImpactHandler = clickHandler;
            _parent = parent;
            _maxVolumeCells = maxVolumeCells;
            _prefabHeight = prefabHeight;

            _gameHandler.PauseStateChanged += OnGamePause;

            //CreateCells(maxVolumeCells);
        }

        public void CreateCell(Color color)
        {
            if (CanAddCell() == false)
                return;

            float currentY = _cellsStack.Count * _prefabHeight;

            MagicCell cell = _factory.CreateCell(
                parent: _parent,
                localPosition: new Vector3(0, currentY, 0),
                color: color);

            cell.Interacted += OnCellClicked;

            _cellsStack.Push(cell);
        }

        public bool CanAddCell()
        {
            return _cellsStack.Count < _maxVolumeCells;
        }

        public MagicCell TryGetCellByColor(Color color)
        {
            MagicCell topCell = GetUpperCell();

            if (topCell == null)
                return null;

            if (topCell.Color == color)
                return topCell;

            return null;
        }

        public bool CheckSecondCell(Color color)
        {
            MagicCell secondCell = GetSecondCell();

            if (secondCell == null)
                return false;

            return secondCell.Color == color;
        }

        public MagicCell GetBottomCell()
        {
            if (_cellsStack.Count == 0)
                return null;

            MagicCell bottomCell = null;

            foreach (MagicCell cell in _cellsStack)
                bottomCell = cell;

            return bottomCell;
        }

        public MagicCell GetUpperCell()
        {
            if (_cellsStack.TryPeek(out MagicCell cell))
                return cell;

            return null;
        }

        private MagicCell GetSecondCell()
        {
            if (_cellsStack.Count < 2)
                return null;

            MagicCell topCell = _cellsStack.Pop();
            MagicCell secondCell = _cellsStack.Peek();

            _cellsStack.Push(topCell);

            return secondCell;
        }

        private void OnGamePause(bool isPaused)
        {
            _isPaused = isPaused;
        }

        private void ClearStack()
        {
            while (_cellsStack.Count > 0)
            {
                MagicCell cell = _cellsStack.Pop();

                cell.Interacted -= OnCellClicked;

                if (cell != null)
                    Destroy(cell.gameObject);
            }
        }

        public void AcceptCell(MagicCell cell)
        {
            if (cell == null)
                throw new ArgumentNullException(nameof(cell));

            _cellsStack.Push(cell);
        }

        private void CreateCells(int countCells)
        {
            float currentY = 0f;

            for (int i = 0; i < countCells; i++)
            {
                //if (_colorSource.TryGetRandomColor(out Color pickedColor) == false)
                //    return;

                ColorEntry entry = _listColorPool.Get();

                if (entry == null)
                    return;

                Color pickedColor = entry.ConsumeColor();

                MagicCell cell = _factory.CreateCell(
                    parent: _parent,
                    localPosition: new Vector3(0, currentY, 0),
                    color: pickedColor);

                cell.Interacted += OnCellClicked;

                _cellsStack.Push(cell);

                currentY += _prefabHeight;
            }
        }

        private void OnCellClicked()
        {
            if (_isPaused)
                return;

            if (_clickImpactHandler.CurrentMode == ClickImpactMode.ModeDistribution)
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
            else if (_clickImpactHandler.CurrentMode == ClickImpactMode.ModeReverse)
            {
                _cellsStack = _columnRevercer.ReverseStack(_cellsStack.ToArray());

                _clickImpactHandler.Reverse();
                _clickImpactHandler.OnToggleMode();
            }
        }

        private void ValidateArguments(
        MagicCellsFactory factory,
        MagicCellRouter cellRouter,
        ShuffledColorDistributor colorSource,
        ClickModeSwitcher clickImpactHandler,
        Transform parent)
        {
            if (factory == null)
                throw new ArgumentNullException(nameof(factory));

            if (cellRouter == null)
                throw new ArgumentNullException(nameof(cellRouter));

            if (colorSource == null)
                throw new ArgumentNullException(nameof(colorSource));

            if (clickImpactHandler == null)
                throw new ArgumentNullException(nameof(clickImpactHandler));

            if (parent == null)
                throw new ArgumentNullException(nameof(parent));
        }

        private void ValidateValues(int countCells, float prefabHeight)
        {
            if (countCells <= 0)
                throw new ArgumentException(nameof(countCells));

            if (prefabHeight <= 0)
                throw new ArgumentException(nameof(prefabHeight));
        }
    }
}
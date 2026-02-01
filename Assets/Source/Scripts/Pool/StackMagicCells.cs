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
        private ClickImpactHandler _clickImpactHandler;
        private Transform _parent;
        private float _prefabHeight;
        private bool _isPaused;
        private Stack<MagicCell> _cellsStack = new();

        public void Initialize(
            MagicCellsFactory factory,
            MagicCellRouter cellRouter,
            ShuffledColorDistributor colorSource,
            ClickImpactHandler clickHandler,
            Transform parent,
            int countCells,
            float prefabHeight)
        {
            ValidateArguments(factory, cellRouter, colorSource, clickHandler, parent);
            ValidateValues(countCells, prefabHeight);

            _factory = factory;
            _cellRouter = cellRouter;
            _colorSource = colorSource;
            _clickImpactHandler = clickHandler;
            _parent = parent;
            _prefabHeight = prefabHeight;

            _gameHandler.PauseStateChanged += OnGamePause;

            CreateCells(countCells);
        }

        private void OnDisable()
        {
            _gameHandler.PauseStateChanged -= OnGamePause;
        }

        private void OnGamePause(bool isPaused)
        {
            _isPaused = isPaused;
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
            else if (_clickImpactHandler.CurrentMode == ClickImpactMode.ModeReverce)
            {
                _cellsStack = _columnRevercer.ReverseStack(_cellsStack.ToArray());

                _clickImpactHandler.Reverse();
                _clickImpactHandler.ToggleMode();
            }
        }

        private void ValidateArguments(
        MagicCellsFactory factory,
        MagicCellRouter cellRouter,
        ShuffledColorDistributor colorSource,
        ClickImpactHandler clickImpactHandler,
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
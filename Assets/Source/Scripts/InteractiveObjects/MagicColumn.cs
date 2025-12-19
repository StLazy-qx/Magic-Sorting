using Assets.Source.Scripts.ActionHandlers;
using Assets.Source.Scripts.Colorize;
using Assets.Source.Scripts.Factory;
using Assets.Source.Scripts.MagicCells;
using Assets.Source.Scripts.Pool;
using System;
using UnityEngine;

namespace Assets.Source.Scripts.InteractiveObjects
{
    public class MagicColumn : MonoBehaviour, IInteractable
    {
        [SerializeField] private StackMagicCellsHandler _stackHandler;

        private int _countCells;
        private float _prefabHeight;
        private float _distanceBetweenCells = 0.05f;
        private ShuffledColorDistributor _colorSource;
        private MagicCellRouter _cellRouter;
        private MagicCellsFactory _factory;

        public event Action Interacted;

        private void Awake()
        {
            _factory = GetComponent<MagicCellsFactory>();

            ValidateObjects();
        }

        public void Initialize(MagicCellRouter distributerMagicCell,
            ShuffledColorDistributor colorSource, int countCells)
        {
            if (countCells <= 0)
            {
                throw new ArgumentException(
                    "Count cells must be positive", nameof(countCells));
            }

            _cellRouter = distributerMagicCell ??
                throw new ArgumentNullException(nameof(distributerMagicCell));
            _cellRouter = distributerMagicCell;
            _colorSource = colorSource;
            _countCells = countCells;

            CreateStackHandler();
        }

        public void OnClick()
            => Interacted?.Invoke();

        private void CreateStackHandler()
        {
            _prefabHeight = _factory.GetCellHeight() + _distanceBetweenCells;

            if (_prefabHeight <= 0)
            {
                throw new InvalidOperationException(
                    "Calculated prefab height must be positive");
            }

            _stackHandler.Initialize(
                _factory,
                _cellRouter,
                _colorSource,
                transform,
                _countCells,
                _prefabHeight
                );
        }

        private void ValidateObjects()
        {
            if (_stackHandler == null)
            {
                throw new InvalidOperationException(
                    $"{nameof(_stackHandler)} must be assigned in inspector");
            }

            if (_factory == null)
            {
                throw new InvalidOperationException(
                    $"Failed to get {nameof(_factory)} from component");
            }
        }
    }
}
using System;
using UnityEngine;
using ActionHandler;
using Colorize;
using FactoryCore;
using MagicCells;
using Pool;

namespace InteractiveObjects
{
    public class MagicColumn : MonoBehaviour, IInteractable
    {
        [SerializeField] private MagicCellsStackHandler _stackHandler;

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
        }

        public void Initialize(MagicCellRouter distributerMagicCell,
            ShuffledColorDistributor colorSource, int countCells)
        {
            if (countCells <= 0 || colorSource == null)
                return;

            _cellRouter = distributerMagicCell;
            _colorSource = colorSource;
            _countCells = countCells;

            CreateStackHandler();
        }

        public void OnClick()
        {
            Interacted?.Invoke();
        }

        private void CreateStackHandler()
        {
            _prefabHeight = _factory.GetCellHeight() + _distanceBetweenCells;

            _stackHandler.Initialize(
                _factory,
                _cellRouter,
                _colorSource,
                transform,
                _countCells,
                _prefabHeight
                );
        }
    }
}
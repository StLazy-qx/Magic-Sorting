using Assets.Source.Scripts.ActionsHandlers;
using Assets.Source.Scripts.Enums;
using Assets.Source.Scripts.MagicCells;
using System;
using UnityEngine;

namespace Assets.Source.Scripts.InteractiveObjects
{
    public class WaitingPoint : MonoBehaviour
    {
        private const float RotationX = 90f;

        [SerializeField] private int _seatsNumber;
        [SerializeField] private Transform _storagePoint;
        [SerializeField] private MagicCellRouter _cellRouter;
        [SerializeField] private ClickImpactHandler _clickImpactHandler;

        private MagicCell _waitingCell;

        public Vector3 CellPosition => _waitingCell.transform.position;
        public bool IsFreePlace { get; private set; }

        private void Awake()
        {
            ValidateObjects();
            Reset();
        }

        public void AcceptStorageCell(MagicCell cell)
        {
            if (cell == null)
            {
                throw new ArgumentNullException(nameof(cell),
                    "[WaitingPoint] Волшебная ячейка не может быть нулевой.");
            }

            if (_waitingCell != null && IsFreePlace == true)
                return;

            IsFreePlace = false;
            Quaternion cellRotation = Quaternion.Euler(RotationX, 0f, 0f);

            _waitingCell = Instantiate(
                cell, 
                _storagePoint.position, 
                cellRotation);

            _waitingCell.Interacted += OnCellClicked;
        }

        public void Reset()
        {
            IsFreePlace = true;

            if (_waitingCell != null)
            {
                Destroy(_waitingCell.gameObject);

                _waitingCell = null;
            }
        }

        private void OnCellClicked()
        {
            if (_clickImpactHandler.CurrentMode == ClickImpactMode.ModeDistribution)
            {
                MagicCell waitCell = _waitingCell;

                if (_cellRouter.IsCheckCellColor(waitCell.Color) == false)
                    return;

                _cellRouter.DeliverMagicCell(waitCell);
                _waitingCell.Disable();

                _waitingCell.Interacted -= OnCellClicked;

                Reset();
            }
        }

        private void ValidateObjects()
        {
            if (_seatsNumber <= 0)
            {
                throw new InvalidOperationException(
                    $"[WaitingPoint] Некорректное количество мест: " +
                    $"{_seatsNumber}. Должно быть положительным числом.");
            }

            if (_storagePoint == null)
            {
                throw new MissingReferenceException(
                    "[WaitingPoint] Не назначена точка хранения (_storagePoint).");
            }

            if (_cellRouter == null)
            {
                throw new MissingReferenceException(
                    "[WaitingPoint] Не назначен маршрутизатор ячеек (_cellRouter).");
            }
        }
    }
}
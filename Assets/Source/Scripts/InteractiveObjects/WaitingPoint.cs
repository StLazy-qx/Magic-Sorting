using Assets.Source.Scripts.ActionHandlers;
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

        private MagicCell _waitingCell;
        private ClickHandler _clickHandler;

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
            _waitingCell = Instantiate(cell, _storagePoint.position, cellRotation);
            _clickHandler = _waitingCell.GetComponent<ClickHandler>();

            if (_clickHandler == null)
            {
                throw new MissingComponentException(
                    "[WaitingPoint] На ячейке отсутствует компонент ClickHandler.");
            }

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

            _clickHandler.OnClicked -= OnCellClicked;

            Reset();
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
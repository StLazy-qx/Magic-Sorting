using Assets.Source.Scripts.Extensions;
using Assets.Source.Scripts.InteractiveObjects;
using Assets.Source.Scripts.Vessels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Source.Scripts.MagicCells
{
    public class MagicCellRouter : MonoBehaviour
    {
        private const float DeliveryDelay = 1.2f;

        [SerializeField] private WaitingPoint _waitingPoint;

        private WaitForSeconds _deliveryWait;
        private IReadOnlyList<Vessel> _vessels;

        public event Action<Vector3, Vector3, Color> CellDelivering;
        public event Action CellDeparturing;

        private void Awake()
        {
            Guard.NotNull(_waitingPoint, nameof(_waitingPoint));

            _deliveryWait = new WaitForSeconds(DeliveryDelay);
        }

        public void Initialize(IReadOnlyList<Vessel> vessels)
        {
            Guard.NotNull(vessels, nameof(vessels));
            Guard.IsTrue(vessels.Count > 0, nameof(vessels),
                "[MagicCellRouter] Список судов не может быть пустым.");

            _vessels = vessels;
        }

        public void DeliverMagicCell(MagicCell cell)
        {
            Guard.NotNull(cell, nameof(cell));
            CellDeparturing?.Invoke();

            Vessel targetVessel = FindVesselByColor(cell.Color);

            if (targetVessel != null)
            {
                Deliver(cell, targetVessel.transform);
                targetVessel.TakeMagic(cell);                                                                                                                         
            }
            else if (_waitingPoint.IsFreePlace)
            {
                Deliver(cell, _waitingPoint.transform);
                _waitingPoint.AcceptStorageCell(cell);

                StartCoroutine(ExecuteAfterDelay(
                    _waitingPoint.ShowWaitingCell));
            }
        }

        public bool IsCheckCellColor(Color color)
        {
            return FindVesselByColor(color) != null 
                || _waitingPoint.IsFreePlace;
        }

        private void Deliver(MagicCell cell, Transform target)
        {
            CellDelivering?.Invoke(
                cell.transform.position,
                target.position,
                cell.Color);
        }

        private IEnumerator ExecuteAfterDelay(Action action)
        {
            yield return _deliveryWait;

            action?.Invoke();
        }

        private Vessel FindVesselByColor(Color color)
        {
            return _vessels.FirstOrDefault(
                vessel => vessel.IsActive 
                && vessel.Color == color);
        }
    }
}
using Assets.Source.Scripts.Colorize;
using Assets.Source.Scripts.MagicCells;
using System;
using UnityEngine;

namespace Assets.Source.Scripts.Vessels
{
    [RequireComponent(typeof(VolumeAggregator))]

    public class Vessel : MonoBehaviour, IColorable, IVesselable
    {
        [SerializeField] private Liquid _liquid;
        [SerializeField] private int _maxSize;
        [SerializeField] private int _points;

        private Color _mainColor;
        private VolumeAggregator _aggregator;

        public event Action<Vector3> Filled;
        public event Action<Vector3, int, Color> RewardIssued;

        public int Count => _maxSize;
        public bool IsActive => gameObject.activeSelf;
        public Color Color => _mainColor;
        public Liquid Liquid => _liquid;
        public bool IsFilled { get; private set; }

        private void Awake()
        {
            ValidateInitializeArguments();

            _aggregator = GetComponent<VolumeAggregator>();

            if (_aggregator == null)
            {
                throw new NullReferenceException(
                    "VolumeAggregator component is missing on Vessel.");
            }

            _aggregator.InitParameters(_maxSize, _liquid);

            IsFilled = false;
        }

        public void TakeMagic(MagicCell cell)
        {
            if (cell == null)
                return;

            _aggregator.GrowUpVolume();

            if (_aggregator.IsFull)
            {
                IsFilled = true;

                RewardIssued?.Invoke(transform.position, _points, _mainColor);
                Filled?.Invoke(transform.position);
                gameObject.SetActive(false);
            }
        }

        public void SetColor(Color color)
            => _mainColor = color;

        private void ValidateInitializeArguments()
        {
            if (_liquid == null)
            {
                throw new NullReferenceException(
                    "Liquid reference is missing in Vessel.");
            }

            if (_maxSize <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(_maxSize),
                    "Max size must be greater than zero.");
            }

            if (_points < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(_points),
                    "Points cannot be negative.");
            }
        }
    }
}
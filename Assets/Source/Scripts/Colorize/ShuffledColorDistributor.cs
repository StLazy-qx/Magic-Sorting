using Assets.Source.Scripts.Pool;
using Assets.Source.Scripts.Vessels;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Source.Scripts.Colorize
{
    public class ShuffledColorDistributor : MonoBehaviour
    {
        [SerializeField] private ParticlePool _particlePool;

        private IReadOnlyList<Vessel> _vessels;
        private List<Color> _colors = new List<Color>();
        private Queue<Color> _mixedColors = new Queue<Color>();

        public int TotalColors => _colors.Count;

        public void Initialize(IReadOnlyList<Vessel> vessels)
        {
            ValidateVessels(vessels);

            _vessels = vessels;

            GenerateColorList();
            ShuffleColors();
        }

        public bool TryGetRandomColor(out Color color)
        {
            return _mixedColors.TryDequeue(out color);
        }

        private void GenerateColorList()
        {
            _colors.Clear();

            foreach (Vessel vessel in _vessels)
            {
                if (vessel.Count <= 0)
                {
                    throw new InvalidOperationException
                        ($"Vessel count must be positive, but was {vessel.Count}");
                }

                for (int i = 0; i < vessel.Count; i++)
                {
                    _colors.Add(vessel.Color);
                }
            }
        }

        private void ShuffleColors()
        {
            int startRandomRange = 0;
            int stepIndex = 1;

            _particlePool.Initialize(TotalColors);

            for (int i = _colors.Count - 1; i > 0; i--)
            {
                int randomNumber = UnityEngine.Random.Range(startRandomRange, i + stepIndex);

                Color tempColor = _colors[i];
                _colors[i] = _colors[randomNumber];
                _colors[randomNumber] = tempColor;
            }

            _mixedColors.Clear();

            foreach (Color color in _colors)
                _mixedColors.Enqueue(color);
        }

        private void ValidateVessels(IReadOnlyList<Vessel> vessels)
        {
            if (vessels == null)
                throw new ArgumentNullException(nameof(vessels), "The list of vessels must be initialized");

            if (vessels.Count == 0)
                throw new ArgumentException("The list of vessels cannot be empty", nameof(vessels));

            if (vessels.Any(vessel => vessel == null))
                throw new ArgumentException("The vessel list contains a zero element", nameof(vessels));
        }
    }
}
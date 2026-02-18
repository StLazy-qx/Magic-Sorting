using Assets.Source.Scripts.Vessels;
using System;
using UnityEngine;

namespace Assets.Source.Scripts.Colorize
{
    class ColorMarkerMix : MonoBehaviour
    {
        [SerializeField] private MixVessel _mixVessel;
        [SerializeField] private Flag _flag;

        private Liquid _liquid;

        private void Awake()
        {
            ValidateObjects();
        }

        private void OnEnable()
        {
            _mixVessel.ColorChanged += OnSetColor;
        }

        private void OnDisable()
        {
            _mixVessel.ColorChanged -= OnSetColor;
        }

        public void OnSetColor(Color color)
        {
            if (_mixVessel != null)
                _liquid = _mixVessel.Liquid;

            if (_mixVessel != null)
                _mixVessel.SetColor(color);

            if (_liquid != null)
                _liquid.SetColor(color);

            if (_flag != null)
                _flag.SetColor(color);
        }

        private void ValidateObjects()
        {
            if (_mixVessel == null)
            {
                throw new ArgumentNullException
                    (nameof(_mixVessel), "Vessel cannot be null");
            }

            if (_flag == null)
            {
                throw new ArgumentNullException
                    (nameof(_flag), "Flag cannot be null");
            }
        }
    }
}

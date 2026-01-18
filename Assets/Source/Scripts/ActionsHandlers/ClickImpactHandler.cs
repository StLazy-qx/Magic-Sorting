using Assets.Source.Scripts.Enums;
using Assets.Source.Scripts.UI.Tutorial;
using System;
using UnityEngine;

namespace Assets.Source.Scripts.ActionsHandlers
{
    public class ClickImpactHandler : MonoBehaviour
    {
        [SerializeField] private ActionButton _modeButton;

        public event Action<ClickImpactMode> ModeChanged;

        public ClickImpactMode CurrentMode { get; private set; }

        private void Awake()
        {
            ActivateDistributionMode();
            _modeButton.OnClick.AddListener(ToggleMode);
        }

        private void ToggleMode()
        {
            if (CurrentMode == ClickImpactMode.ModeDistribution)
                ActivateReverceMode();
            else
                ActivateDistributionMode();
        }

        private void ActivateDistributionMode()
        {
            CurrentMode = ClickImpactMode.ModeDistribution;

            ModeChanged?.Invoke(CurrentMode);
        }

        private void ActivateReverceMode()
        {
            CurrentMode = ClickImpactMode.ModeReverce;

            ModeChanged?.Invoke(CurrentMode);
        }
    }
}

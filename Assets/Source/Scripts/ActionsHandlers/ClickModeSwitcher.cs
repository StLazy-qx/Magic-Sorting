using Assets.Source.Scripts.Enums;
using Assets.Source.Scripts.UI.Buttons;
using System;
using UnityEngine;

namespace Assets.Source.Scripts.ActionsHandlers
{
    public class ClickModeSwitcher : MonoBehaviour
    {
        private ReverseButton _reverceButton;
        private ButtonRewardedAdv _rewardedButton;
        private bool _isReverseUsed;

        public event Action<ClickImpactMode> ModeChanged;

        public ClickImpactMode CurrentMode { get; private set; }

        private void Awake()
        {
            ActivateDistributionMode();
        }

        public void Reset()
        {
            _isReverseUsed = false;

            _reverceButton.ResetState();
            _rewardedButton.Disable();
            ActivateDistributionMode();
            UpdateButtonsState();
        }

        public void SetButton(ReverseButton reverseButton, ButtonRewardedAdv rewardedButton)
        {
            _reverceButton = reverseButton
                ?? throw new ArgumentNullException(nameof(reverseButton));

            _rewardedButton = rewardedButton
                ?? throw new ArgumentNullException(nameof(rewardedButton));

            _reverceButton.OnClick.AddListener(ToggleMode);
        }

        public void Reverse()
        {
            if (CurrentMode != ClickImpactMode.ModeReverce)
                return;

            _isReverseUsed = true;

            _rewardedButton.Enable();
            UpdateButtonsState();
        }

        public void ToggleMode()
        {
            if (_isReverseUsed &&
                CurrentMode == ClickImpactMode.ModeReverce)
            {
                ActivateDistributionMode();
                return;
            }

            if (CurrentMode == ClickImpactMode.ModeDistribution)
                ActivateReverceMode();
            else
                ActivateDistributionMode();
        }

        private void UpdateButtonsState()
        {
            bool shouldDisable =
                (_isReverseUsed && CurrentMode ==
                ClickImpactMode.ModeReverce)
                || (_isReverseUsed && CurrentMode ==
                ClickImpactMode.ModeDistribution);

            _reverceButton.ResetState();
            _rewardedButton.ResetState();

            //if (shouldDisable)
            //    _reverceButton.Disable();
            //else
            //    _reverceButton.Enable();
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

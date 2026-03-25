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

            _reverceButton.OnClick.AddListener(OnToggleMode);
            _rewardedButton.OnClick.AddListener(OnRewardedClicked);
        }

        public void Reverse()
        {
            if (CurrentMode != ClickImpactMode.ModeReverce)
                return;

            _isReverseUsed = true;

            _rewardedButton.Enable();
            UpdateButtonsState();
        }

        public void OnToggleMode()
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
            if (_isReverseUsed)
            {
                _reverceButton.Disable();
                _reverceButton.SetState(false);
                _rewardedButton.Enable();
            }
            else
            {
                _reverceButton.Enable();
                _rewardedButton.Disable();
            }
        }

        private void OnRewardedClicked()
        {
            _rewardedButton.SetState(false);
            _reverceButton.Enable();
            _reverceButton.SetState(true);

            ActivateReverceMode();
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

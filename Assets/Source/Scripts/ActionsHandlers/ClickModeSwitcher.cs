using Assets.Source.Scripts.Enums;
using Assets.Source.Scripts.UI.Buttons;
using System;
using UnityEngine;
using YG;

namespace Assets.Source.Scripts.ActionsHandlers
{
    public class ClickModeSwitcher : MonoBehaviour
    {
        public string RewardID;
        private ReverseButton _reverceButton;
        private IconRewardedAdvertisement _rewardedIcon;
        private bool _isReverseUsed;
        private bool _reverseEventFired;
        private bool _isShowingReward;

        public event Action<ClickImpactMode> ModeChanged;
        public event Action ReverseButtonActivating;

        public ClickImpactMode CurrentMode { get; private set; }

        private void Awake()
        {
            ActivateDistributionMode();
        }

        public void Reset()
        {
            _isReverseUsed = false;
            _reverseEventFired = false;

            _reverceButton.ResetState();
            _rewardedIcon.Disable();
            ActivateDistributionMode();
            UpdateButtonsState();
        }

        public void SetButton(ReverseButton reverseButton, IconRewardedAdvertisement rewardedButton)
        {
            _reverceButton = reverseButton
                ?? throw new ArgumentNullException(nameof(reverseButton));

            _rewardedIcon = rewardedButton
                ?? throw new ArgumentNullException(nameof(rewardedButton));

            _reverceButton.OnClick.AddListener(OnToggleMode);
        }

        public void Reverse()
        {
            if (CurrentMode != ClickImpactMode.ModeReverse)
                return;

            _isReverseUsed = true;

            _rewardedIcon.Enable();
            UpdateButtonsState();
        }

        public void OnToggleMode()
        {
            if (CurrentMode == ClickImpactMode.ModeReverse)
            {
                ActivateDistributionMode();

                return;
            }

            if (_isReverseUsed)
            {
                if (_rewardedIcon.gameObject.activeSelf == false)
                {
                    ActivateReverceMode();

                    return;
                }

                ShowReward();

                //YG2.RewardedAdvShow(RewardID, () =>
                //{
                //    ActivateReverceMode();
                //});
            }
            else
            {
                ActivateReverceMode();
            }
        }

        private void ShowReward()
        {
            if (_isShowingReward)
                return;

            _isShowingReward = true;

            _reverceButton.UIButton.interactable = false;

            YG2.RewardedAdvShow(RewardID, () =>
            {
                _isShowingReward = false;
                _reverceButton.Enable();

                ActivateReverceMode();

                _reverceButton.UIButton.interactable = true;
            });
        }

        private void UpdateButtonsState()
        {
            if (_isReverseUsed)
            {
                _reverceButton.Disable();
                _reverceButton.SetState(false);
                _rewardedIcon.Enable();
            }
            else
            {
                _reverceButton.Enable();
                _rewardedIcon.Disable();
            }
        }

        private void ActivateDistributionMode()
        {
            CurrentMode = ClickImpactMode.ModeDistribution;

            ModeChanged?.Invoke(CurrentMode);
        }

        private void ActivateReverceMode()
        {
            CurrentMode = ClickImpactMode.ModeReverse;

            ModeChanged?.Invoke(CurrentMode);

            if (_reverseEventFired == false)
            {
                ReverseButtonActivating?.Invoke();

                _reverseEventFired = true;
            }
        }
    }
}

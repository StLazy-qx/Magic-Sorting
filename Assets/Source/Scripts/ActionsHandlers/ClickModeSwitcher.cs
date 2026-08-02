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
        private ReverseButton _reverseButton;
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

            _reverseButton.ResetState();
            _rewardedIcon.Disable();
            ActivateDistributionMode();
            UpdateButtonState();
        }

        public void SetButton(ReverseButton reverseButton, IconRewardedAdvertisement rewardedButton)
        {
            _reverseButton = reverseButton
                ?? throw new ArgumentNullException(nameof(reverseButton));

            _rewardedIcon = rewardedButton
                ?? throw new ArgumentNullException(nameof(rewardedButton));

            _reverseButton.OnClick.AddListener(OnToggleMode);
        }

        public void Reverse()
        {
            if (CurrentMode != ClickImpactMode.ModeReverse)
                return;

            _isReverseUsed = true;

            UpdateButtonState();
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
            _reverseButton.UIButton.interactable = false;

            YG2.onErrorRewardedAdv += OnRewardError;

            YG2.RewardedAdvShow(RewardID, () =>
            {
                YG2.onErrorRewardedAdv -= OnRewardError;

                _isShowingReward = false;

                _reverseButton.Enable();
                ActivateReverceMode();
                _reverseButton.SetState(true);

                _reverseButton.UIButton.interactable = true;
            });

            //YG2.RewardedAdvShow(RewardID, () =>
            //{
            //    _isShowingReward = false;
            //    _reverseButton.Enable();

            //    ActivateReverceMode();

            //    _reverseButton.UIButton.interactable = true;
            //});
        }

        private void UpdateButtonState()
        {
            if (_isReverseUsed)
            {
                _reverseButton.Disable();
                _reverseButton.SetState(false);
                _rewardedIcon.Enable();
            }
            else
            {
                _reverseButton.Enable();
                _rewardedIcon.Disable();
            }
        }

        private void OnRewardError()
        {
            YG2.onErrorRewardedAdv -= OnRewardError;

            _isShowingReward = false;
            _reverseButton.UIButton.interactable = true;
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

using Assets.Source.Scripts.Enums;
using Assets.Source.Scripts.UI.Buttons;
using Assets.Source.Scripts.UI.GameModeView;
using Assets.Source.Scripts.YG;
using System;
using UnityEngine;
using YG;

namespace Assets.Source.Scripts.ActionsHandlers
{
    public class ClickModeSwitcher : MonoBehaviour
    {
        //[SerializeField] private string rewardID;

        //private ReverseButtonView _reverseButtonView;
        //private readonly ClickModeTracker _clickModeTracker = new ClickModeTracker();
        //private readonly RewardedadvertisingGateway _adGateway = new RewardedadvertisingGateway();

        //public event Action RewardedEnded;
        //public event Action ReverseButtonActivating;

        //public ClickImpactMode CurrentMode => _clickModeTracker.CurrentMode;

        //private void Awake()
        //{
        //    _clickModeTracker.ReverseButtonActivated += () => ReverseButtonActivating?.Invoke();
        //    _clickModeTracker.ActivateDistributionMode();
        //}

        //private void OnDestroy()
        //{
        //    _clickModeTracker.ReverseButtonActivated -= () => ReverseButtonActivating?.Invoke();

        //    if (_reverseButtonView != null)
        //        _reverseButtonView.ButtonClicked -= OnToggleMode;
        //}

        //public void SetButton(ReverseButtonView reverseButtonView)
        //{
        //    _reverseButtonView = reverseButtonView
        //        ?? throw new ArgumentNullException(nameof(reverseButtonView));

        //    _reverseButtonView.ButtonClicked += OnToggleMode;
        //}

        //public void Reset()
        //{
        //    _clickModeTracker.Reset();
        //    _reverseButtonView.ResetState();
        //    UpdateView();
        //}

        //public void Reverse()
        //{
        //    if (_clickModeTracker.CurrentMode != ClickImpactMode.ModeReverse)
        //        return;

        //    _clickModeTracker.MarkReverseUsed();
        //    UpdateView();
        //}

        //public void OnToggleMode()
        //{
        //    if (_clickModeTracker.CurrentMode == ClickImpactMode.ModeReverse)
        //    {
        //        _clickModeTracker.ActivateDistributionMode();
        //        return;
        //    }

        //    if (_clickModeTracker.IsReverseUsed)
        //    {
        //        if (_reverseButtonView.IsRewardedIconActive == false)
        //        {
        //            _clickModeTracker.ActivateReverseMode();
        //            return;
        //        }

        //        ShowReward();
        //    }
        //    else
        //    {
        //        _clickModeTracker.ActivateReverseMode();
        //    }
        //}

        //private void ShowReward()
        //{
        //    _reverseButtonView.SetButtonInteractable(false);

        //    void OnRewardFinished() => RewardedEnded?.Invoke();

        //    _adGateway.ShowReward(
        //        rewardID,
        //        onSuccess: () =>
        //        {
        //            _reverseButtonView.EnableButton();
        //            _reverseButtonView.SetButtonInteractable(true);
        //            _clickModeTracker.ActivateReverseMode();
        //        },
        //        onError: () =>
        //        {
        //            _reverseButtonView.SetButtonInteractable(true);
        //            _reverseButtonView.SetReverseUsedState(false);
        //        },
        //        onClose: () =>
        //        {
        //            _reverseButtonView.SetButtonInteractable(true);
        //            _reverseButtonView.SetReverseUsedState(true);
        //        }
        //    );
        //}

        //private void UpdateView()
        //{
        //    _reverseButtonView.SetReverseUsedState(_clickModeTracker.IsReverseUsed);
        //}

        public string RewardID;
        private ReverseButton _reverseButton;
        private IconRewardedAdvertisement _rewardedIcon;
        private bool _isReverseUsed;
        private bool _reverseEventFired;
        private bool _isShowingReward;

        public event Action<ClickImpactMode> ModeChanged;
        public event Action ReverseButtonActivating;
        public event Action RewardedEnded;

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
            YG2.onCloseRewardedAdv += OnRewardClose;

            YG2.RewardedAdvShow(RewardID, () =>
            {
                YG2.onErrorRewardedAdv -= OnRewardError;
                YG2.onCloseRewardedAdv -= OnRewardClose;

                _isShowingReward = false;

                _reverseButton.Enable();
                ActivateReverceMode();

                _reverseButton.UIButton.interactable = true;
            });

            RewardedEnded?.Invoke();
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

            _reverseButton.SetState(false);
        }

        private void OnRewardClose()
        {
            YG2.onErrorRewardedAdv -= OnRewardError;
            YG2.onCloseRewardedAdv -= OnRewardClose;

            _isShowingReward = false;
            _reverseButton.UIButton.interactable = true;

            _reverseButton.SetState(true);
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

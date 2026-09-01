using Assets.Source.Scripts.Enums;
using Assets.Source.Scripts.UI.GameModeView;
using Assets.Source.Scripts.YG;
using System;
using UnityEngine;

namespace Assets.Source.Scripts.ActionsHandlers
{
    public class ClickModeSwitcher : MonoBehaviour
    {
        public string RewardID;

        private readonly ClickModeTracker _modeTracker = new ClickModeTracker();
        private ReverseButtonView _reverseButtonView;
        private ReverseRewardFlow _rewardFlow;

        public event Action<ClickImpactMode> ModeChanged
        {
            add => _modeTracker.ModeChanged += value;
            remove => _modeTracker.ModeChanged -= value;
        }

        public event Action ReverseButtonActivating
        {
            add => _modeTracker.ReverseButtonActivated += value;
            remove => _modeTracker.ReverseButtonActivated -= value;
        }

        public event Action RewardedEnded;

        public ClickImpactMode CurrentMode => _modeTracker.CurrentMode;

        private void Awake()
        {
            _modeTracker.ActivateDistributionMode();
        }

        public void Reset()
        {
            _modeTracker.Reset();
            _reverseButtonView.ResetState();
            UpdateButtonState();
        }

        public void SetButton(ReverseButtonView reverseButtonView)
        {
            _reverseButtonView = reverseButtonView
                ?? throw new ArgumentNullException(nameof(reverseButtonView));

            _rewardFlow = new ReverseRewardFlow(
                _modeTracker,
                new RewardedadvertisingGateway(),
                _reverseButtonView,
                RewardID);

            _rewardFlow.Ended += () => RewardedEnded?.Invoke();
            _reverseButtonView.ButtonClicked += OnToggleMode;
        }

        public void Reverse()
        {
            if (CurrentMode != ClickImpactMode.ModeReverse)
                return;

            _modeTracker.MarkReverseUsed();
            UpdateButtonState();
        }

        public void OnToggleMode()
        {
            if (CurrentMode == ClickImpactMode.ModeReverse)
            {
                _modeTracker.ActivateDistributionMode();

                return;
            }

            if (_modeTracker.IsReverseUsed)
            {
                if (_reverseButtonView.IsRewardedIconActive == false)
                {
                    _modeTracker.ActivateReverseMode();

                    return;
                }

                _rewardFlow.Show();
            }
            else
            {
                _modeTracker.ActivateReverseMode();
            }
        }

        private void UpdateButtonState()
        {
            _reverseButtonView.SetReverseUsedState(_modeTracker.IsReverseUsed);
        }

        ////public string RewardID;

        ////private readonly ClickModeTracker _modeTracker = new ClickModeTracker();
        ////private readonly RewardedadvertisingGateway _rewardedGateway = new RewardedadvertisingGateway();
        ////private ReverseButtonView _reverseButtonView;

        ////public event Action<ClickImpactMode> ModeChanged
        ////{
        ////    add => _modeTracker.ModeChanged += value;
        ////    remove => _modeTracker.ModeChanged -= value;
        ////}

        ////public event Action ReverseButtonActivating
        ////{
        ////    add => _modeTracker.ReverseButtonActivated += value;
        ////    remove => _modeTracker.ReverseButtonActivated -= value;
        ////}

        ////public event Action RewardedEnded;

        ////public ClickImpactMode CurrentMode => _modeTracker.CurrentMode;

        ////private void Awake()
        ////{
        ////    _modeTracker.ActivateDistributionMode();
        ////}

        ////public void Reset()
        ////{
        ////    _modeTracker.Reset();

        ////    _reverseButtonView.ResetState();
        ////    UpdateButtonState();
        ////}

        ////public void SetButton(ReverseButtonView reverseButtonView)
        ////{
        ////    _reverseButtonView = reverseButtonView
        ////        ?? throw new ArgumentNullException(nameof(reverseButtonView));

        ////    _reverseButtonView.ButtonClicked += OnToggleMode;
        ////}

        ////public void Reverse()
        ////{
        ////    if (CurrentMode != ClickImpactMode.ModeReverse)
        ////        return;

        ////    _modeTracker.MarkReverseUsed();

        ////    UpdateButtonState();
        ////}

        ////public void OnToggleMode()
        ////{
        ////    if (CurrentMode == ClickImpactMode.ModeReverse)
        ////    {
        ////        _modeTracker.ActivateDistributionMode();

        ////        return;
        ////    }

        ////    if (_modeTracker.IsReverseUsed)
        ////    {
        ////        if (_reverseButtonView.IsRewardedIconActive == false)
        ////        {
        ////            _modeTracker.ActivateReverseMode();

        ////            return;
        ////        }

        ////        ShowReward();
        ////    }
        ////    else
        ////    {
        ////        _modeTracker.ActivateReverseMode();
        ////    }
        ////}

        ////private void ShowReward()
        ////{
        ////    if (_rewardedGateway.IsShowing)
        ////        return;

        ////    _reverseButtonView.SetButtonInteractable(false);

        ////    _rewardedGateway.ShowReward(
        ////        RewardID,
        ////        onSuccess: OnRewardSuccess,
        ////        onError: OnRewardError,
        ////        onClose: OnRewardClose);

        ////    RewardedEnded?.Invoke();
        ////}

        ////private void OnRewardSuccess()
        ////{
        ////    _reverseButtonView.EnableButton();
        ////    _modeTracker.ActivateReverseMode();
        ////    _reverseButtonView.SetButtonInteractable(true);
        ////}

        ////private void OnRewardError()
        ////{
        ////    _reverseButtonView.SetButtonInteractable(true);
        ////    _reverseButtonView.SetReverseUsedState(false);
        ////}

        ////private void OnRewardClose()
        ////{
        ////    _reverseButtonView.SetButtonInteractable(true);
        ////    _reverseButtonView.SetReverseUsedState(true);
        ////}

        ////private void UpdateButtonState()
        ////{
        ////    _reverseButtonView.SetReverseUsedState(_modeTracker.IsReverseUsed);
        ////}

        //public string RewardID;
        //private ReverseButton _reverseButton;
        //private IconRewardedAdvertisement _rewardedIcon;
        //private bool _isReverseUsed;
        //private bool _reverseEventFired;
        //private bool _isShowingReward;

        //public event Action<ClickImpactMode> ModeChanged;
        //public event Action ReverseButtonActivating;
        //public event Action RewardedEnded;

        //public ClickImpactMode CurrentMode { get; private set; }

        //private void Awake()
        //{
        //    ActivateDistributionMode();
        //}

        //public void Reset()
        //{
        //    _isReverseUsed = false;
        //    _reverseEventFired = false;

        //    _reverseButton.ResetState();
        //    _rewardedIcon.Disable();
        //    ActivateDistributionMode();
        //    UpdateButtonState();
        //}

        //public void SetButton(ReverseButton reverseButton, IconRewardedAdvertisement rewardedButton)
        //{
        //    _reverseButton = reverseButton
        //        ?? throw new ArgumentNullException(nameof(reverseButton));

        //    _rewardedIcon = rewardedButton
        //        ?? throw new ArgumentNullException(nameof(rewardedButton));

        //    _reverseButton.OnClick.AddListener(OnToggleMode);
        //}

        //public void Reverse()
        //{
        //    if (CurrentMode != ClickImpactMode.ModeReverse)
        //        return;

        //    _isReverseUsed = true;

        //    UpdateButtonState();
        //}

        //public void OnToggleMode()
        //{
        //    if (CurrentMode == ClickImpactMode.ModeReverse)
        //    {
        //        ActivateDistributionMode();

        //        return;
        //    }

        //    if (_isReverseUsed)
        //    {
        //        if (_rewardedIcon.gameObject.activeSelf == false)
        //        {
        //            ActivateReverceMode();

        //            return;
        //        }

        //        ShowReward();
        //    }
        //    else
        //    {
        //        ActivateReverceMode();
        //    }
        //}

        //private void ShowReward()
        //{
        //    if (_isShowingReward)
        //        return;

        //    _isShowingReward = true;
        //    _reverseButton.UIButton.interactable = false;

        //    YG2.onErrorRewardedAdv += OnRewardError;
        //    YG2.onCloseRewardedAdv += OnRewardClose;

        //    YG2.RewardedAdvShow(RewardID, () =>
        //    {
        //        YG2.onErrorRewardedAdv -= OnRewardError;
        //        YG2.onCloseRewardedAdv -= OnRewardClose;

        //        _isShowingReward = false;

        //        _reverseButton.Enable();
        //        ActivateReverceMode();

        //        _reverseButton.UIButton.interactable = true;
        //    });

        //    RewardedEnded?.Invoke();
        //}

        //private void UpdateButtonState()
        //{
        //    if (_isReverseUsed)
        //    {
        //        _reverseButton.Disable();
        //        _reverseButton.SetState(false);
        //        _rewardedIcon.Enable();
        //    }
        //    else
        //    {
        //        _reverseButton.Enable();
        //        _rewardedIcon.Disable();
        //    }
        //}

        //private void OnRewardError()
        //{
        //    YG2.onErrorRewardedAdv -= OnRewardError;

        //    _isShowingReward = false;
        //    _reverseButton.UIButton.interactable = true;

        //    _reverseButton.SetState(false);
        //}

        //private void OnRewardClose()
        //{
        //    YG2.onErrorRewardedAdv -= OnRewardError;
        //    YG2.onCloseRewardedAdv -= OnRewardClose;

        //    _isShowingReward = false;
        //    _reverseButton.UIButton.interactable = true;

        //    _reverseButton.SetState(true);
        //}

        //private void ActivateDistributionMode()
        //{
        //    CurrentMode = ClickImpactMode.ModeDistribution;

        //    ModeChanged?.Invoke(CurrentMode);
        //}

        //private void ActivateReverceMode()
        //{
        //    CurrentMode = ClickImpactMode.ModeReverse;

        //    ModeChanged?.Invoke(CurrentMode);

        //    if (_reverseEventFired == false)
        //    {
        //        ReverseButtonActivating?.Invoke();

        //        _reverseEventFired = true;
        //    }
        //}
    }
}

using Assets.Source.Scripts.UI.Buttons;
using System;
using UnityEngine;

namespace Assets.Source.Scripts.UI.GameModeView
{
    public class ReverseButtonView : MonoBehaviour
    {
        [SerializeField] private ReverseButton _reverseButton;
        [SerializeField] private IconRewardedAdvertisement _rewardedIcon;

        public event Action ButtonClicked;

        public bool IsRewardedIconActive =>
            _rewardedIcon != null && _rewardedIcon.gameObject.activeSelf;

        public void SetReverseUsedState(bool isUsed)
        {
            if (isUsed)
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

        public void EnableButton()
        {
            if (_reverseButton != null)
                _reverseButton.Enable();
        }

        public void SetButtonInteractable(bool interactable)
            => _reverseButton.UIButton.interactable = interactable;

        public void ResetState()
            => SetReverseUsedState(false);
    }
}

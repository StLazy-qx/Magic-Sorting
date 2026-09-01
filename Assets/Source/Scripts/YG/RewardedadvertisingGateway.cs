using System;
using YG;

namespace Assets.Source.Scripts.YG
{
    public class RewardedadvertisingGateway
    {
        public bool IsShowing { get; private set; }

        private Action _currentOnError;
        private Action _currentOnClose;

        public void ShowReward(
            string rewardId, 
            Action onSuccess,
            Action onError, 
            Action onClose)
        {
            if (IsShowing)
                return;

            IsShowing = true;
            _currentOnError = () => CompleteReward(onError);
            _currentOnClose = () => CompleteReward(onClose);

            void HandleSuccess() => CompleteReward(onSuccess);

            Subscribe(_currentOnError, _currentOnClose);

            YG2.RewardedAdvShow(rewardId, HandleSuccess);
        }

        private void CompleteReward(Action callback)
        {
            Unsubscribe();

            IsShowing = false;

            callback?.Invoke();
        }

        private void Subscribe(Action onError, Action onClose)
        {
            YG2.onErrorRewardedAdv += onError;
            YG2.onCloseRewardedAdv += onClose;
        }

        private void Unsubscribe()
        {
            if (_currentOnError != null)
            {
                YG2.onErrorRewardedAdv -= _currentOnError;
                _currentOnError = null;
            }

            if (_currentOnClose != null)
            {
                YG2.onCloseRewardedAdv -= _currentOnClose;
                _currentOnClose = null;
            }
        }
    }
}

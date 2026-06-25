using Assets.Source.Scripts.Extensions;
using Assets.Source.Scripts.UI.Buttons;
using UnityEngine;

namespace Assets.Source.Scripts.GameBehaviour
{
    public class RewardedAdvertisementHandler : MonoBehaviour 
    {
        private const int FirstRoundNumber = 1;

        [SerializeField] private LevelCounter _levelCounter;
        [SerializeField] private IconRewardedAdvertisement _iconView;

        private void Awake()
        {
            Guard.NotNull(_levelCounter, nameof(_levelCounter));
            Guard.NotNull(_iconView, nameof(_iconView));
            _iconView.gameObject.SetActive(false);

            _levelCounter.RoundChanged += OnShowIconReward;
        }

        private void OnDisable()
        {
            if (_levelCounter != null)
                _levelCounter.RoundChanged -= OnShowIconReward;
        }

        private void OnShowIconReward(int value)
        {
            if (value > FirstRoundNumber)
                _iconView.gameObject.SetActive(true);
        }
    }
}

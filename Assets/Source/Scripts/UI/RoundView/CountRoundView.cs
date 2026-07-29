using Assets.Source.Scripts.GameBehaviour;
using Assets.Source.Scripts.Extensions;
using UnityEngine;
using TMPro;
using DG.Tweening;

namespace Assets.Source.Scripts.UI.RoundView
{
    public class CountRoundView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _countText;
        [SerializeField] private LevelCounter _levelCounter;
        [SerializeField] private GameSessionHandler _sessionHandler;

        private void Awake()
        {
            Guard.NotNull(_countText, nameof(_countText));
            Guard.NotNull(_levelCounter, nameof(_levelCounter));
            Guard.NotNull(_sessionHandler, nameof(_sessionHandler));
        }

        private void Start()
        {
            OnShowRoundNumberWithFade();
        }

        private void OnEnable()
        {
            _sessionHandler.GameLaunching += OnShowRoundNumberWithFade;
            _levelCounter.RoundChanged += OnCountTextChanged;
        }

        private void OnDisable()
        {
            _sessionHandler.GameLaunching -= OnShowRoundNumberWithFade;
            _levelCounter.RoundChanged -= OnCountTextChanged;
        }

        public void OnShowRoundNumberWithFade()
        {
            float initialAlpha = 1f;
            float targetAlpha = 0f;
            float fadeDuration = 2.5f;
            float fadeDelay = 0.5f;

            _countText.DOKill();

            _countText.alpha = initialAlpha;
            _countText.DOFade(targetAlpha, fadeDuration)
                .SetDelay(fadeDelay)
                .SetEase(Ease.OutQuad);
        }

        private void OnCountTextChanged(int roundNumber)
            => _countText.text = roundNumber.ToString();
    }
}

using Assets.Source.Scripts.GameBehaviour;
using UnityEngine;
using TMPro;
using Assets.Source.Scripts.Extensions;

namespace Assets.Source.Scripts.UI.RoundView
{
    public class CountRoundView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _countText;
        [SerializeField] private LevelCounter _levelCounter;

        private void Awake()
        {
            Guard.NotNull(_countText, nameof(_countText));
            Guard.NotNull(_levelCounter, nameof(_levelCounter));
        }

        private void OnEnable()
        {
            _levelCounter.RoundChanged += OnCountTextChanged;
        }

        private void OnDisable()
        {
            _levelCounter.RoundChanged -= OnCountTextChanged;
        }

        private void OnCountTextChanged(int roundNumber)
        {
            _countText.text = roundNumber.ToString();
        }
    }
}

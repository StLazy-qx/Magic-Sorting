using Assets.Source.Scripts.GameBehaviour;
using UnityEngine;
using System;
using TMPro;

namespace Assets.Source.Scripts.UI.RoundView
{
    public class RoundView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _countText;
        [SerializeField] private GameSessionHandler _gameSessionHandler;

        private void Awake()
        {
            if (_countText == null)
                throw new ArgumentNullException(nameof(_countText));

            if (_gameSessionHandler == null)
                throw new ArgumentNullException(nameof(_gameSessionHandler));
        }

        private void OnEnable()
        {
            _gameSessionHandler.RoundChanged += OnCountTextChanged;
        }

        private void OnDisable()
        {
            _gameSessionHandler.RoundChanged -= OnCountTextChanged;
        }

        private void OnCountTextChanged(int roundNumber)
        {
            _countText.text = roundNumber.ToString();
        }
    }
}

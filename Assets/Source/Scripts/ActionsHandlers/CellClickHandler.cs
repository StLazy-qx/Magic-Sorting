using Assets.Source.Scripts.Enums;
using Assets.Source.Scripts.GameBehaviour;
using System;
using UnityEngine;

namespace Assets.Source.Scripts.ActionsHandlers
{
    public class CellClickHandler : MonoBehaviour
    {
        private const int LeftMouseButton = 0;

        [SerializeField] private GameSessionHandler _gameHandler;

        private ClickImpactHandler _clickImpactHandler;
        private bool _canClick = true;
        private bool _isPaused;

        public event Action OnClicked;

        private void Awake()
        {
            if (_gameHandler == null)
                throw new ArgumentNullException(nameof(_gameHandler));

            _clickImpactHandler = _gameHandler.ClickImpactHandler;

            if (_clickImpactHandler == null)
                throw new ArgumentNullException(nameof(_clickImpactHandler));
        }

        private void OnEnable()
        {
            _gameHandler.PauseStateChanged += OnPauseStateChanged;
            _clickImpactHandler.ModeChanged += OnModeChanged;

            OnModeChanged(_clickImpactHandler.CurrentMode);
        }

        private void OnDisable()
        {
            _gameHandler.PauseStateChanged -= OnPauseStateChanged;
            _clickImpactHandler.ModeChanged -= OnModeChanged;
        }

        private void OnMouseDown()
        {
            if (_canClick && Input.GetMouseButtonDown(LeftMouseButton))
            {
                OnClicked?.Invoke();
            }
        }

        private void OnPauseStateChanged(bool isPaused)
        {
            //_canClick = !isPaused;
            _canClick = isPaused;

            UpdateCanClick();
        }

        private void OnModeChanged(ClickImpactMode mode)
        {
            _canClick = mode == ClickImpactMode.ModeDistribution;

            UpdateCanClick();
        }

        private void UpdateCanClick()
        {
            _canClick = !_isPaused &&
                        _clickImpactHandler.CurrentMode == ClickImpactMode.ModeDistribution;
        }
    }
}

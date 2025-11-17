using System;
using UnityEngine;
using GameBehaviour;

namespace ActionHandler
{
    public class ClickHandler : MonoBehaviour
    {
        private const int LeftMouseButton = 0;

        [SerializeField] private GameSessionHandler _gameHandler;

        private bool _canClick = true;

        public event Action OnClicked;

        private void Awake()
        {
            if (_gameHandler == null)
            {
                throw new ArgumentNullException
                    (nameof(_gameHandler), "GameSessionHandler cannot be null");
            }
        }

        private void OnEnable()
        {
            _gameHandler.PauseStateChanged += OnPauseStateChanged;
        }

        private void OnDisable()
        {
            _gameHandler.PauseStateChanged -= OnPauseStateChanged;
        }

        private void OnMouseDown()
        {
            if (_canClick && Input.GetMouseButtonDown(LeftMouseButton))
                OnClicked?.Invoke();
        }

        private void OnPauseStateChanged(bool isPaused)
        {
            _canClick = !isPaused;
        }
    }
}

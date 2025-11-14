using System;
using UnityEngine;

public class ClickHandler : MonoBehaviour
{
    [SerializeField] private GameHandler _gameHandler;

    public event Action OnClicked;

    private bool _canClick = true;

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
        if (_canClick && Input.GetMouseButtonDown(0))
        {
            OnClicked?.Invoke();
        }
    }

    private void OnPauseStateChanged(bool isPaused)
    {
        _canClick = !isPaused;
    }
}

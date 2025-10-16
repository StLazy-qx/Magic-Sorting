using System;
using UnityEngine;

public class FinalGameSession : MonoBehaviour
{
    [SerializeField] private Panel _finalMatchPanelDesctop;
    [SerializeField] private Panel _finalMatchPanelMobile;
    [SerializeField] private GameHandler _gameHandler;

    private Panel _currentPanel;

    public event Action<int> RoundChanged;

    public int CurrentRound { get; private set; }

    public void UseDesctopPanel()
    {
        _currentPanel = _finalMatchPanelDesctop;
    }

    public void UseMobilePanel()
    {
        _currentPanel = _finalMatchPanelMobile;
    }

    public void ActivateFinalPanelAndPauseGame()
    {
        if (_gameHandler == null)
            return;

        _gameHandler.PauseGame();
        _currentPanel.Open();
        IncreaseRound();
    }

    public void DeactivateFinalPanelAndResumeGame()
    {
        if (_gameHandler == null)
            return;

        _currentPanel.Close();
        _gameHandler.ContinueGame();
    }

    private void IncreaseRound()
    {
        CurrentRound++;
        RoundChanged?.Invoke(CurrentRound);
    }
}

using System;
using UnityEngine;
using EntryPoint;

namespace GameBehaviour
{
    public class FinalGameSession : MonoBehaviour, IObjectInitilizable
    {
        [SerializeField] private Panel _finalMatchPanelDesctop;
        [SerializeField] private Panel _finalMatchPanelMobile;
        [SerializeField] private GameSessionHandler _gameHandler;

        private Panel _currentPanel;

        public event Action<int> RoundChanged;

        public int CurrentRound { get; private set; }
        public bool IsInitialized { get; private set; }

        public void Initilize()
        {
            if (IsInitialized)
                return;

            if (_gameHandler == null)
                return;

            if (_currentPanel == null)
                return;

            _currentPanel.Close();

            CurrentRound = 0;

            IsInitialized = true;
        }

        public void ApplyPanel(Panel panel)
        {
            if (panel == null)
            {
                throw new ArgumentNullException(nameof(panel),
                    "[VesselStateTracker] Панель не может быть нуль");
            }

            _currentPanel = panel;
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
}
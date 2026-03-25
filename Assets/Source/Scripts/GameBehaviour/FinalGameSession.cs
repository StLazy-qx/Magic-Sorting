using System;
using UnityEngine;
using Assets.Source.Scripts.EntryPoint;
using Assets.Source.Scripts.UI.GamePanel;

namespace Assets.Source.Scripts.GameBehaviour
{
    public class FinalGameSession : MonoBehaviour, IObjectInitilizable
    {
        [SerializeField] private Panel _finalMatchPanelDesctop;
        [SerializeField] private Panel _finalMatchPanelMobile;
        [SerializeField] private GameSessionHandler _gameHandler;

        private Panel _currentPanel;

        public bool IsInitialized { get; private set; }

        public void Initialize()
        {
            ValidateObjects();
            _currentPanel.Close();

            IsInitialized = true;
        }

        public void ApplyPanel(Panel panel)
        {
            if (panel == null)
                throw new ArgumentNullException(nameof(panel));

            _currentPanel = panel;
        }

        public void ActivatePanel()
        {
            _gameHandler.PauseGame();
            _currentPanel.Open();
        }

        public void DeactivateFinalPanelAndResumeGame()
        {
            if (_gameHandler == null)
                return;

            _currentPanel.Close();
            _gameHandler.ContinueGame();
        }

        private void ValidateObjects()
        {
            if (_gameHandler == null)
                throw new ArgumentNullException(nameof(_gameHandler));

            if (_finalMatchPanelDesctop == null)
                throw new ArgumentNullException(nameof(_finalMatchPanelDesctop));

            if (_finalMatchPanelMobile == null)
                throw new ArgumentNullException(nameof(_finalMatchPanelMobile));
        }
    }
}
using Assets.Source.Scripts.EntryPoint;
using Assets.Source.Scripts.UI.GamePanel;
using Assets.Source.Scripts.Extensions;
using System;
using UnityEngine;

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

        public void ShowEndRoundPanel()
        {
            _gameHandler.PauseGame();
            _currentPanel.Open();
        }

        private void ValidateObjects()
        {
            Guard.NotNull(_gameHandler, nameof(_gameHandler));
            Guard.NotNull(_finalMatchPanelDesctop, nameof(_finalMatchPanelDesctop));
            Guard.NotNull(_finalMatchPanelMobile, nameof(_finalMatchPanelMobile));
        }
    }
}
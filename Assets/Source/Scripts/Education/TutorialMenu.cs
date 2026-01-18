using Assets.Source.Scripts.UI.GamePanel;
using Assets.Source.Scripts.UI.Tutorial;
using System;
using UnityEngine;

namespace Assets.Source.Scripts.Education
{
    class TutorialMenu : MonoBehaviour
    {
        private const int FirstPanelIndex = 0;

        [SerializeField] private ActionButton _nextButton;
        [SerializeField] private ActionButton _backButton;
        [SerializeField] private Panel _menuPanel;
        [SerializeField] private Panel[] _panels;

        private int _currentIndex;

        private void Awake()
        {
            _nextButton.OnClick.AddListener(OnNextPanel);
            _backButton.OnClick.AddListener(OnPreviousPanel);

            ResetFirstPanel();
            UpdateButtons();
        }

        private void ShowPanel(int index)
        {
            if (index < 0 || index >= _panels.Length)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(index),index,
                    $"Index must be in range [0, " +
                    $"{_panels.Length - 1}]");
            }

            CloseAllPanels();
            _panels[index].Open();

            _currentIndex = index;

            UpdateButtons();
        }

        private void ResetFirstPanel()
        {
            CloseAllPanels();

            _currentIndex = FirstPanelIndex;

            _panels[_currentIndex].Open();
        }

        private void CloseAllPanels()
        {
            foreach (Panel panel in _panels)
                panel.Close();
        }

        private void OnNextPanel()
        {
            if (_currentIndex < _panels.Length - 1)
                ShowPanel(_currentIndex + 1);
        }

        private void OnPreviousPanel()
        {
            if (_currentIndex > FirstPanelIndex)
                ShowPanel(_currentIndex - 1);
        }

        private void UpdateButtons()
        {
            _backButton.GetComponent<UnityEngine.UI.Button>()
                .interactable = _currentIndex > FirstPanelIndex;
            _nextButton.GetComponent<UnityEngine.UI.Button>()
                .interactable = _currentIndex < _panels.Length - 1;
        }
    }
}

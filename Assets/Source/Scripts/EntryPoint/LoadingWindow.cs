using Assets.Source.Scripts.UI.GamePanel;
using UnityEngine;
using YG;

namespace Assets.Source.Scripts.EntryPoint
{
    public class LoadingWindow : MonoBehaviour
    {
        [SerializeField] private Panel _desktopPanel;
        [SerializeField] private Panel _mobilePanel;

        private Panel _currentPanel;

        public static LoadingWindow Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);

                return;
            }

            Instance = this;

            DontDestroyOnLoad(gameObject);

            if (YG2.envir.isMobile)
            {
                _currentPanel = _mobilePanel;
            }
            else
            {
                _currentPanel = _desktopPanel;
            }

            Hide();
        }

        public void SetParent(Transform parentTransform)
        {
            transform.SetParent(parentTransform);
        }

        public void Show()
        {
            if (_currentPanel != null)
                _currentPanel.gameObject.SetActive(true);
        }

        public void Hide()
        {
            if (_currentPanel != null)
                _currentPanel.gameObject.SetActive(false);
        }
    }
}
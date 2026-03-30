using Assets.Source.Scripts.Language;
using Assets.Source.Scripts.SceneManagement;
using Assets.Source.Scripts.UI.LanguageView;
using System;
using UnityEngine;

namespace Assets.Source.Scripts.EntryPoint
{
    public class PlatformMenuAdapter : BasePlatformAdapter
    {
        [Header("Panels")]
        [SerializeField] private LanguageView _desktopLanguageView;
        [SerializeField] private LanguageView _mobileLanguageView;
        [Header("Loading Windows")]
        //[SerializeField] private Sprite _mobileLoadindgImage;
        //[SerializeField] private Sprite _desktopLoadingImage;
        [SerializeField] private LoadingWindow _mobileLoadindgPanel;
        [SerializeField] private LoadingWindow _desktopLoadindgPanel;

        private LanguageSetter _languageSetter;
        //private LoadingWindow _loadingWindow;
        private SceneLoader _sceneLoader;

        public void Initialize(
            //LoadingWindow loadingWindow,
            LanguageSetter languageSetter)
        {
            ValidateDependencies(/*loadingWindow,*/ languageSetter);

            _sceneLoader = new SceneLoader();
            _languageSetter = languageSetter;
            //_loadingWindow = loadingWindow;

            InitializeBase();
        }

        protected override void OnMobileSelected()
        {
            _mobileLanguageView.Initialize(_languageSetter);
        }

        protected override void OnDesktopSelected()
        {
            _desktopLanguageView.Initialize(_languageSetter);

            //_loadingWindow.transform.SetParent(DesktopCanvas.transform, false);
            //_sceneLoader.Initialize(_loadingWindow);
        }

        private void ValidateDependencies(
            LanguageSetter languageSetter)
        {
            if (languageSetter == null)
                throw new ArgumentNullException(nameof(languageSetter));

            if (_desktopLanguageView == null)
                throw new ArgumentNullException(nameof(_desktopLanguageView));

            if (_mobileLanguageView == null)
                throw new ArgumentNullException(nameof(_mobileLanguageView));
        }
    }
}
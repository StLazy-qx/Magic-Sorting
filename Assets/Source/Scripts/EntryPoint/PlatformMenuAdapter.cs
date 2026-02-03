using Assets.Source.Scripts.Language;
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

        private LanguageSetter _languageSetter;

        public void Initialize(LoadingWindow loadingWindow, LanguageSetter languageSetter)
        {
            if (loadingWindow == null)
                throw new ArgumentNullException(nameof(loadingWindow));

            if (languageSetter == null)
                throw new ArgumentNullException(nameof(languageSetter));

            if (_desktopLanguageView == null)
                throw new ArgumentNullException(nameof(_desktopLanguageView));

            if (_mobileLanguageView == null)
                throw new ArgumentNullException(nameof(_mobileLanguageView));

            _languageSetter = languageSetter;

            InitializeBase(loadingWindow);
        }

        protected override void OnMobileSelected()
        {
            _mobileLanguageView.Initialize(_languageSetter);
        }

        protected override void OnDesktopSelected()
        {
            _desktopLanguageView.Initialize(_languageSetter);
        }
    }
}
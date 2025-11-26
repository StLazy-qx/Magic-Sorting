using Assets.Source.Scripts.Language;
using Assets.Source.Scripts.Audio;
using System;
using UnityEngine;
using Assets.Source.Scripts.UI.LanguageView;

namespace Assets.Source.Scripts.EntryPoint
{
    public class PlatformMenuAdapter : PlatformAdapter
    {
        [Header("Panels")]
        [SerializeField] private LanguageView _languageViewDesktop;
        [SerializeField] private LanguageView _languageViewMobile;

        private LanguageSetter _languageSetter;

        public void Initialize(LanguageSetter languageSetter)
        {
            if (languageSetter == null)
                throw new ArgumentNullException(nameof(languageSetter));

            if (_languageViewDesktop == null)
                throw new ArgumentNullException(nameof(_languageViewDesktop));

            if (_languageViewMobile == null)
                throw new ArgumentNullException(nameof(_languageViewMobile));

            _languageSetter = languageSetter;

            InitializeBase();
        }

        protected override void OnMobileSelected()
        {
            _languageViewMobile.Initialize(_languageSetter);
        }

        protected override void OnDesktopSelected()
        {
            _languageViewDesktop.Initialize(_languageSetter);
        }
    }
}
using Language;
using System;
using UnityEngine;

namespace EntryPoint
{
    public class PlatformDependentMenuSetter : PlatformDependentBase
    {
        [Header("Panels")]
        [SerializeField] private LanguageView _languageViewDesktop;
        [SerializeField] private LanguageView _languageViewMobile;

        private LanguageSetter _languageSetter;

        public void Initialize(LanguageSetter languageSetter)
        {
            if (languageSetter == null)
                throw new ArgumentException("Значение не может быть равным или меньше нуля");

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
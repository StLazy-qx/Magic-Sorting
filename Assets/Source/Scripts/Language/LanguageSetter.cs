using System;
using YG;

namespace Assets.Source.Scripts.Language
{
    public class LanguageSetter
    {
        public event Action<string> OnLanguageChanged;

        public string CurrentLanguage { get; private set; }

        public LanguageSetter()
        {
            CurrentLanguage = YG2.lang;
        }

        public void SetLanguage(string language)
        {
            ValidateString(language);

            if (CurrentLanguage == language)
                return;

            CurrentLanguage = language;

            YG2.SwitchLanguage(language);
            OnLanguageChanged?.Invoke(language);
        }

        private void ValidateString(string language)
        {
            if (string.IsNullOrWhiteSpace(language))
            {
                throw new ArgumentException(
                    "Language cannot be null, empty or whitespace.", nameof(language));
            }
        }
    }
}
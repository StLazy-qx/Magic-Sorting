using System;
using YG;

public class LanguageSetter
{
    public string CurrentLanguage { get; private set; }

    public event Action<string> OnLanguageChanged;

    public LanguageSetter(string initialLanguage)
    {
        CurrentLanguage = initialLanguage;
    }

    public void SetLanguage(string language)
    {
        if (CurrentLanguage == language)
            return;

        CurrentLanguage = language;

        YG2.SwitchLanguage(language);
        OnLanguageChanged?.Invoke(language);
    }
}

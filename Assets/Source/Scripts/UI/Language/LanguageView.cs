using UnityEngine;
using UnityEngine.UI;
using Language;

public class LanguageView : MonoBehaviour
{
    private const string RussianLanguage = "ru";
    private const string EnglishLanguage = "en";
    private const string TurkishLanguage = "tr";

    [SerializeField] private Button _englishButton;
    [SerializeField] private Button _russianButton;
    [SerializeField] private Button _turkishButton;
    [SerializeField] private Color _selectedColor;

    private Color _defaultColorButton;
    private LanguageSetter _languageSetter;

    public void Initialize(LanguageSetter setter)
    {
        _languageSetter = setter;
        _defaultColorButton = _englishButton.image.color;
        _languageSetter.OnLanguageChanged += UpdateUI;

        UpdateUI(_languageSetter.CurrentLanguage);
    }

    private void OnEnable()
    {
        _russianButton.onClick.AddListener(() => _languageSetter.SetLanguage(RussianLanguage));
        _englishButton.onClick.AddListener(() => _languageSetter.SetLanguage(EnglishLanguage));
        _turkishButton.onClick.AddListener(() => _languageSetter.SetLanguage(TurkishLanguage));
    }

    private void OnDisable()
    {
        _russianButton.onClick.RemoveAllListeners();
        _englishButton.onClick.RemoveAllListeners();
        _turkishButton.onClick.RemoveAllListeners();
    }

    private void UpdateUI(string langCode)
    {
        ResetButtonColors();

        switch (langCode)
        {
            case RussianLanguage:
                Highlight(_russianButton);
                break;
            case EnglishLanguage:
                Highlight(_englishButton);
                break;
            case TurkishLanguage:
                Highlight(_turkishButton);
                break;
        }
    }

    private void Highlight(Button button)
    {
        button.image.color = _selectedColor;
    }

    private void ResetButtonColors()
    {
        _russianButton.image.color = _defaultColorButton;
        _englishButton.image.color = _defaultColorButton;
        _turkishButton.image.color = _defaultColorButton;
    }
}

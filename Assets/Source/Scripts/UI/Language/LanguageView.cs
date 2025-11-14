using UnityEngine;
using UnityEngine.UI;

public class LanguageView : MonoBehaviour
{
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
        _russianButton.onClick.AddListener(() => _languageSetter.SetLanguage("ru"));
        _englishButton.onClick.AddListener(() => _languageSetter.SetLanguage("en"));
        _turkishButton.onClick.AddListener(() => _languageSetter.SetLanguage("tr"));
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
            case "ru":
                Highlight(_russianButton);
                break;
            case "en":
                Highlight(_englishButton);
                break;
            case "tr":
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

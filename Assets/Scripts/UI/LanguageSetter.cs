using UnityEngine;
using UnityEngine.UI;
using YG;

public class LanguageSetter : MonoBehaviour
{
    [SerializeField] private Button _englishButton;
    [SerializeField] private Button _russianButton;
    [SerializeField] private Button _turkishButton;
    [SerializeField] private Color _selectedColor;

    private Button _selectedButton;
    private Color _defaultColorButton;
    private string _currentLang;

    private void Awake()
    {
        _currentLang = YG2.lang;

        _defaultColorButton = _englishButton.image.color;
    }

    //нужно в через точку входа задавать язык в методе иницилизации
    private void Start()
    {
        OnLanguageChanged(_currentLang);
    }

    private void OnEnable()
    {
        _russianButton.onClick.AddListener(SetRussian);
        _englishButton.onClick.AddListener(SetEnglish);
        _turkishButton.onClick.AddListener(SetTurkish);
    }

    private void OnDisable()
    {
        _russianButton.onClick.RemoveListener(SetRussian);
        _englishButton.onClick.RemoveListener(SetEnglish);
        _turkishButton.onClick.RemoveListener(SetTurkish);
    }

    private void SetRussian() 
        => SetLanguage("ru");

    private void SetEnglish() 
        => SetLanguage("en");

    private void SetTurkish() 
        => SetLanguage("tr");

    private void SetLanguage(string code)
    {
        YG2.SwitchLanguage(code);

        OnLanguageChanged(code);
    }

    //название метода поменять- это подсветка выбранного языкаы
    private void OnLanguageChanged(string langCode)
    {
        switch (langCode)
        {
            case "ru":
                HighlightButton(_russianButton);
                break;
            case "en":
                HighlightButton(_englishButton);
                break;
            case "tr":
                HighlightButton(_turkishButton);
                break;
            default:
                ResetButtonColors();
                break;
        }
    }

    private void HighlightButton(Button button)
    {
        ResetButtonColors();

        button.image.color = _selectedColor;
        _selectedButton = button;
    }

    private void ResetButtonColors()
    {
        if (_russianButton != null)
            _russianButton.image.color = _defaultColorButton;

        if (_englishButton != null)
            _englishButton.image.color = _defaultColorButton;

        if (_turkishButton != null)
            _turkishButton.image.color = _defaultColorButton;
    }
}

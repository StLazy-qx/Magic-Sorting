using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LevelDifficultySetterUI : MonoBehaviour
{
    [SerializeField] private Button _easyLevelButton;
    [SerializeField] private Button _middleLevelButton;
    [SerializeField] private Button _hardLevelButton;
    [SerializeField] private Color _selectedColor;

    private DifficultyState _difficultyState;
    private Button _selectedButton;
    private Color _defaultColorButton;

    private void Awake()
    {
        if (_easyLevelButton != null)
            _defaultColorButton = _easyLevelButton.image.color;
    }

    private void Start()
    {
        OnDifficultyChanged(_difficultyState.CurrentDifficulty);
    }

    private void OnEnable()
    {
        _easyLevelButton.onClick.AddListener(SetEasy);
        _middleLevelButton.onClick.AddListener(SetMedium);
        _hardLevelButton.onClick.AddListener(SetHard);

        _difficultyState.DifficultyChanged += OnDifficultyChanged;
    }

    private void OnDisable()
    {
        _easyLevelButton.onClick.RemoveListener(SetEasy);
        _middleLevelButton.onClick.RemoveListener(SetMedium);
        _hardLevelButton.onClick.RemoveListener(SetHard);

        _difficultyState.DifficultyChanged -= OnDifficultyChanged;
    }

    [Inject]
    public void Construct(DifficultyState state)
    {
        _difficultyState = state;
    }

    public DifficultyLevel Increase(DifficultyLevel currentLevel)
    {
        DifficultyLevel newLevel = currentLevel;

        switch (currentLevel)
        {
            case DifficultyLevel.Easy:
                newLevel = DifficultyLevel.Medium;
                break;
            case DifficultyLevel.Medium:
                newLevel = DifficultyLevel.Hard;
                break;
            case DifficultyLevel.Hard:
                newLevel = DifficultyLevel.Hard; // дальше только изменение количества игровых объектов
                break;
        }

        if (newLevel != currentLevel)
            _difficultyState.SetDifficulty(newLevel);

        return newLevel;
    }

    private void SetEasy() 
        => _difficultyState.SetDifficulty(DifficultyLevel.Easy);

    private void SetMedium() 
        => _difficultyState.SetDifficulty(DifficultyLevel.Medium);

    private void SetHard() 
        => _difficultyState.SetDifficulty(DifficultyLevel.Hard);

    private void OnDifficultyChanged(DifficultyLevel level)
    {
        switch (level)
        {
            case DifficultyLevel.Easy:
                HighlightButton(_easyLevelButton);
                break;
            case DifficultyLevel.Medium:
                HighlightButton(_middleLevelButton);
                break;
            case DifficultyLevel.Hard:
                HighlightButton(_hardLevelButton);
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
        if (_easyLevelButton != null) 
            _easyLevelButton.image.color = _defaultColorButton;

        if (_middleLevelButton != null) 
            _middleLevelButton.image.color = _defaultColorButton;

        if (_hardLevelButton != null) 
            _hardLevelButton.image.color = _defaultColorButton;
    }
}
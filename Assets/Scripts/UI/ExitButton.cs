using UnityEngine;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour 
{
    [SerializeField] private GameHandler _gameHandler;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();

        // Если не назначен в инспекторе — пробуем найти автоматически
        if (_gameHandler == null)
        {
            _gameHandler = FindObjectOfType<GameHandler>();
        }
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        if (_gameHandler != null)
        {
            _gameHandler.QuitGame();
        }
    }
}

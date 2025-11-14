using UnityEngine;
using UnityEngine.UI;
using GameBehaviour;

public class ExitButton : MonoBehaviour 
{
    [SerializeField] private GameHandler _gameHandler;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();

        if (_gameHandler == null)
            return;
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

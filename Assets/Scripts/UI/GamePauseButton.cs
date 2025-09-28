public class GamePauseButton : BaseMenuButton
{
    protected override void OnButtonClick()
    {
        if (GameHandler != null)
        {
            GameHandler.PauseGame();
            CurrentPanel?.Close();
            TargetPanel?.Open();
        }
    }

    //[SerializeField] private GameHandler _gameHandler;
    //[SerializeField] private Panel _currentPanel;
    //[SerializeField] private Panel _targetPanel;
    //[SerializeField] private Button _button;

    //private void OnEnable()
    //{
    //    if (_button != null)
    //    {
    //        _button.onClick.AddListener(OnButtonClick);
    //    }
    //}

    //private void OnDisable()
    //{
    //    if (_button != null)
    //    {
    //        _button.onClick.RemoveListener(OnButtonClick);
    //    }
    //}

    //private void OnButtonClick()
    //{
    //    if (_gameHandler != null)
    //    {
    //        _gameHandler.PauseGame();
    //        _currentPanel.Close();
    //        _targetPanel.Open();
    //    }
    //}
}

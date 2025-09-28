public class GameResumerButton : BaseMenuButton
{
    protected override void OnButtonClick()
    {
        if (GameHandler != null)
        {
            GameHandler.ResumeGame();
            CurrentPanel?.Close();
        }
    }

    //[SerializeField] private GameHandler _gameHandler;
    //[SerializeField] private Panel _currentPanel;
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
    //        _gameHandler.ResumeGame();
    //        _currentPanel.Close();
    //    }
    //}
}

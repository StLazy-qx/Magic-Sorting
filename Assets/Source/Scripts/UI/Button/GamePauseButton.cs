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
}

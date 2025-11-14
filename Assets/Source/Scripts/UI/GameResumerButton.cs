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
}

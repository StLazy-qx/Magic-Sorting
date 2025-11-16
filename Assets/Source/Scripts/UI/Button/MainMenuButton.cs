public class MainMenuButton : BaseMenuButton
{
    protected override void OnButtonClick()
    {
        if (GameHandler != null)
        {
            CurrentPanel?.Close();
            GameHandler.OpenMainMenu();
        }
    }
}

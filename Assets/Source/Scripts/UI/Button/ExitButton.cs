public class ExitButton : BaseMenuButton
{
    protected override void OnButtonClick()
    {
        if (GameHandler == null)
            return;

        GameHandler.QuitGame();
    }
}
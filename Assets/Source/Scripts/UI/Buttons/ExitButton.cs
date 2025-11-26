namespace Assets.Source.Scripts.UI.Buttons
{
    public class ExitButton : BaseMenuButton
    {
        protected override void OnButtonClick()
        {
            GameHandler.QuitGame();
        }
    }
}
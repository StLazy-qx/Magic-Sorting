namespace Assets.Source.Scripts.UI.Buttons
{
    public class MainMenuButton : BaseButton
    {
        protected override void OnButtonClick()
        {
            CurrentPanel?.Close();
            GameHandler.OpenMainMenu();
        }
    }
}
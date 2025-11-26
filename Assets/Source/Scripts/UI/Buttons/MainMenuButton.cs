namespace Assets.Source.Scripts.UI.Buttons
{
    public class MainMenuButton : BaseMenuButton
    {
        protected override void OnButtonClick()
        {
            CurrentPanel?.Close();
            GameHandler.OpenMainMenu();
        }
    }
}
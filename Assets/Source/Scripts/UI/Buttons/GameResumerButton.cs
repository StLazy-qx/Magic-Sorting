namespace Assets.Source.Scripts.UI.Buttons
{
    public class GameResumerButton : BaseMenuButton
    {
        protected override void OnButtonClick()
        {
            GameHandler.ResumeGame();
            CurrentPanel?.Close();
        }
    }
}
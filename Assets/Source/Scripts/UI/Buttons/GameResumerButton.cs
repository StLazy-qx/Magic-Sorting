namespace Assets.Source.Scripts.UI.Buttons
{
    public class GameResumerButton : BaseButton
    {
        protected override void OnButtonClick()
        {
            GameHandler.ResumeGame();
            CurrentPanel?.Close();
        }
    }
}
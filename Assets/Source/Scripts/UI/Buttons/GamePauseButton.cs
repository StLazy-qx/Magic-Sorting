namespace Assets.Source.Scripts.UI.Buttons
{
    public class GamePauseButton : BaseMenuButton
    {
        protected override void OnButtonClick()
        {
            GameHandler.PauseGame();
            CurrentPanel?.Close();
            TargetPanel?.Open();
        }
    }
}
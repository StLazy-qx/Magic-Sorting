namespace Assets.Source.Scripts.UI.Buttons
{
    public class ReturnGameSessionButton : BaseMenuButton
    {
        protected override void OnButtonClick()
        {
            CurrentPanel?.Close();
            TargetPanel?.Open();
            GameHandler.ContinueGame();
        }
    }
}
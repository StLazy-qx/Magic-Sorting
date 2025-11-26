namespace Assets.Source.Scripts.UI.Buttons
{
    public class PanelSwitchButton : BaseMenuButton
    {
        protected override void OnButtonClick()
        {
            CurrentPanel?.Close();
            TargetPanel?.Open();
        }
    }
}
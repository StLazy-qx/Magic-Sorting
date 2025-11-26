using Assets.Source.Scripts.GameBehaviour;

namespace Assets.Source.Scripts.UI.Buttons
{
    public class ButtonNewRoundBeginner : BaseMenuButton
    {
        protected override void OnButtonClick()
        {
            if (GameHandler is GameSessionHandler sessionHandler)
            {
                CurrentPanel?.Close();
                TargetPanel?.Open();
                sessionHandler.BeginNewRound();
            }
        }
    }
}
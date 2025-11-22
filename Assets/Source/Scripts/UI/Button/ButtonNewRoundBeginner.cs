using GameBehaviour;

public class ButtonNewRoundBeginner : BaseMenuButton
{
    protected override void OnButtonClick()
    {
        if (GameHandler == null)
            return;

        if (GameHandler is GameSessionHandler sessionHandler)
        {
            CurrentPanel?.Close();
            TargetPanel?.Open();
            sessionHandler.BeginNewRound();
        }
    }
}

using GameBehaviour;

public class ButtonNewRoundBeginner : BaseMenuButton
{
    protected override void OnButtonClick()
    {
        if (GameHandler == null)
            return;

        if (GameHandler is not GameSessionHandler sessionHandler)
            return;

        CurrentPanel?.Close();
        TargetPanel?.Open();
        sessionHandler.BeginNewRound();
    }
}

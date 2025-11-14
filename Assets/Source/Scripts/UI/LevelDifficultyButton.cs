public class LevelDifficultyButton : BaseMenuButton
{
    protected override void OnButtonClick()
    {
        if (GameHandler != null)
        {
            CurrentPanel?.Close();
            TargetPanel?.Open();
            GameHandler.IncreaseDifficultyLevel();
        }
    }
}

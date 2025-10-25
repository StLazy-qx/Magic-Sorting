using System;
using YG;

public class Wallet
{
    private int _currentScore;

    public int TotalScore => YG2.saves.Points;
    public int CurrentScore => _currentScore;

    public event Action<int> CurrentScoreChanged;
    public event Action<int> TotalScoreChanged;

    private Wallet()
    {
        CurrentScoreChanged?.Invoke(_currentScore);
        TotalScoreChanged?.Invoke(TotalScore);
    }

    public void AddPoints(int value)
    {
        if (value <= 0)
            throw new ArgumentException("Значение не может быть равным или меньше нуля");

        _currentScore += value;

        CurrentScoreChanged?.Invoke(_currentScore);
    }

    public void BuyItem(int value)
    {
        if (value <= 0)
            throw new ArgumentException("Значение не может быть равным или меньше нуля");

        YG2.saves.SubtractPoints(value);

        TotalScoreChanged?.Invoke(TotalScore);
    }

    public void ConfirmPoints()
    {
        YG2.saves.AddPoints(_currentScore);
        _currentScore = 0;

        TotalScoreChanged?.Invoke(TotalScore);
        CurrentScoreChanged?.Invoke(_currentScore);

        //проверить название таблицы
        YG2.SetLeaderboard("MainLeaderboard", YG2.saves.Points);
    }

    public void Reset()
    {
        _currentScore = 0;

        CurrentScoreChanged?.Invoke(_currentScore);
    }

     public bool CanAfford(int value)
    {
        if (value <= 0)
            return false;

        return TotalScore >= value;
    }
}

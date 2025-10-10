using System;
using YG;

public class Wallet
{
    private int _leadboardScore;
    private int _confirmedScore;
    private int _currentScore;

    public int TotalScore => _confirmedScore;
    public int CurrentScore => _currentScore;

    public event Action<int> CurrentScoreChanged;
    public event Action<int> TotalScoreChanged;
    public event Action<int> TableScoreChanged;

    private Wallet()
    {
        CurrentScoreChanged?.Invoke(_currentScore);
        TotalScoreChanged?.Invoke(_confirmedScore);
    }

    public void AddPoints(int value)
    {
        if (value <= 0)
            throw new ArgumentException("Value cannot be negative");

        _currentScore += value;

        CurrentScoreChanged?.Invoke(_currentScore);
    }

    public void BuyItem(int value)
    {
        _confirmedScore -= value;

        TotalScoreChanged?.Invoke(_confirmedScore);
    }

    public void ConfirmPoints()
    {
        _leadboardScore += _currentScore;
        _confirmedScore += _currentScore;
        _currentScore = 0;

        TotalScoreChanged?.Invoke(_confirmedScore);
        CurrentScoreChanged?.Invoke(_currentScore);
        TableScoreChanged?.Invoke(_currentScore);

        //проверить название таблицы
        YG2.SetLeaderboard("MainLeaderboard", _leadboardScore);
    }

    public void Reset()
    {
        _currentScore = 0;

        CurrentScoreChanged?.Invoke(_currentScore);
    }
}

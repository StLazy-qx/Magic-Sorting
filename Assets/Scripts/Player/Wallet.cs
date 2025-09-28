using System;

public class Wallet
{
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
        _confirmedScore += _currentScore;
        _currentScore = 0;

        TotalScoreChanged?.Invoke(_confirmedScore);
        CurrentScoreChanged?.Invoke(_currentScore);
        TableScoreChanged?.Invoke(_currentScore);
    }

    public void Reset()
    {
        _currentScore = 0;

        CurrentScoreChanged?.Invoke(_currentScore);
    }
}

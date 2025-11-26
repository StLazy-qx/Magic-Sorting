using System;
using YG;

namespace Assets.Source.Scripts.Player
{
    public class Wallet
    {
        private int _currentScore;
        private int _totalScore;

        private Wallet()
        {
            _totalScore = YG2.saves.Points;
            CurrentScoreChanged?.Invoke(_currentScore);
            TotalScoreChanged?.Invoke(TotalScore);
        }

        public event Action<int> CurrentScoreChanged;
        public event Action<int> TotalScoreChanged;

        public int TotalScore => _totalScore;

        public void AddPoints(int value)
        {
            if (value < 0)
                throw new ArgumentException("The value cannot be equal to or less than zero.");

            _currentScore += value;

            CurrentScoreChanged?.Invoke(_currentScore);
        }

        public void ConfirmPoints()
        {
            _totalScore += _currentScore;
            _currentScore = 0;

            YG2.saves.SavePoints(_totalScore);
            TotalScoreChanged?.Invoke(TotalScore);
            CurrentScoreChanged?.Invoke(_currentScore);
            YG2.SetLeaderboard("MainLeaderboard", YG2.saves.Points);
        }

        public void BuyItem(int price)
        {
            if (price < 0)
                throw new ArgumentException("The price cannot be less than zero.");

            if (price == 0)
                return;

            if (CanAfford(price) == false)
                return;

            _totalScore -= price;

            YG2.saves.SavePoints(_totalScore);
            TotalScoreChanged?.Invoke(TotalScore);
        }

        public void Reset()
        {
            _currentScore = 0;

            CurrentScoreChanged?.Invoke(_currentScore);
        }

        public bool CanAfford(int value)
        {
            if (value < 0)
                return false;

            return _totalScore >= value;
        }
    }
}
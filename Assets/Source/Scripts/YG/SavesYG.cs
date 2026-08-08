using Assets.Source.Scripts.Extensions;
using System.Collections.Generic;

namespace YG
{
    public partial class SavesYG
    {
        private const int FirstRoundNumber = 1;

        private int _totalPoints;
        private int _points;
        private int _actualRoundNumber;
        private List<string> _itemIDs = new();
        private string _equippedItemID;

        public int Points => _points;
        public string EquippedItemID => _equippedItemID;

        public void SaveScore(int points)
        {
            Guard.NotNegative(points, nameof(points));

            _points = points;
            _totalPoints += points;

            if (YG2.isSDKEnabled)
                YG2.SetLeaderboard("GameLeaderboard", _totalPoints);
        }

        public void DecreaseScore(int points)
        {
            Guard.NotNegative(points, nameof(points));

            _points = points;
        }

        public void AddItem(string itemID)
        {
            Guard.NotNullOrWhiteSpace(itemID, nameof(itemID));

            if (_itemIDs.Contains(itemID))
                return;

            _itemIDs.Add(itemID);
        }

        public void SaveRoundNumber(int number)
        {
            Guard.NotNegative(number, nameof(number));

            if (_actualRoundNumber == number)
                return;

            _actualRoundNumber = number;
        }

        public void SaveEquippedItem(string itemID)
        {
            Guard.NotNullOrWhiteSpace(itemID, nameof(itemID));

            _equippedItemID = string.IsNullOrWhiteSpace(itemID)
                ? string.Empty
                : itemID;
        }

        public bool HasItem(string itemID)
        {
            Guard.NotNullOrWhiteSpace(itemID, nameof(itemID));

            return _itemIDs.Contains(itemID);
        }

        public int GetRoundNumber()
        {
            if (_actualRoundNumber <= 0)
                _actualRoundNumber = FirstRoundNumber;

            return _actualRoundNumber;
        }

        public IReadOnlyList<string> GetPurchasedItems() 
            => _itemIDs.AsReadOnly();
    }
}
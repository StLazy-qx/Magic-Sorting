using Assets.Source.Scripts.Extensions;
using System.Collections.Generic;

namespace YG
{
    public partial class SavesYG
    {
        private int _totalPoints;
        private int _points;
        private List<string> _itemIDs = new();
        private string _equippedItemID;

        public int TotalPoints => _totalPoints;
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

        public IReadOnlyList<string> GetPurchasedItems() 
            => _itemIDs.AsReadOnly();
    }
}
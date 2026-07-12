using Assets.Source.Scripts.Extensions;
using System.Collections.Generic;

namespace YG
{
    public partial class SavesYG
    {
        private int _points;
        private List<string> _itemIDs = new();
        private string _equippedItemID;

        public int Points => _points;

        public void SavePoints(int value)
        {
            Guard.NotNegative(value, nameof(value));

            _points = value;

            if (YG2.isSDKEnabled)
                YG2.SetLeaderboard("GameLeaderboard", value);
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

        public string GetEquippedItemID() 
            => _equippedItemID;

        public IReadOnlyList<string> GetPurchasedItems() 
            => _itemIDs.AsReadOnly();
    }
}
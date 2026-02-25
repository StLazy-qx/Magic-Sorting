using System;
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
			if (value < 0)
            {
                throw new ArgumentException(
                    "The value cannot be equal to or less than zero");
            }

            _points = value;
		}

        public void AddItem(string itemID)
        {
            if (string.IsNullOrWhiteSpace(itemID))
                return;

            if (_itemIDs.Contains(itemID))
                return;

            _itemIDs.Add(itemID);
        }

        public void SaveEquippedItem(string itemID)
        {
            _equippedItemID = string.IsNullOrWhiteSpace(itemID)
                ? string.Empty
                : itemID;
        }

        public bool HasItem(string itemID)
        {
            if (string.IsNullOrWhiteSpace(itemID))
                return false;

            return _itemIDs.Contains(itemID);
        }

        public string GetEquippedItemID() 
            => _equippedItemID;

        public IReadOnlyList<string> GetPurchasedItems() 
            => _itemIDs.AsReadOnly();
    }
}
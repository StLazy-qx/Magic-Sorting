using System;
using System.Collections.Generic;
//using System.Linq;
using Assets.Source.Scripts.Items;

namespace YG
{
    public partial class SavesYG
    {
        private int _points;
		//private string _equippedItemName = string.Empty;
		//private List<Item> _items = new List<Item>();
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

        public void AddItem(Item item)
        {
            if (item == null)
                return;

            if (_itemIDs.Contains(item.ID)) 
                return;

            _itemIDs.Add(item.ID);

            //_items = _items.Where(item => item != null).ToList();

            //if (_items.Any(existingItem => existingItem.name == item.name))
            //    return;

            //_items.Add(item);
        }

        public void SaveEquippedItem(Item item)
        {
            _equippedItemID = item?.ID ?? string.Empty;

            //_equippedItemName = item?.name ?? string.Empty;
        }

        //public Item GetEquippedItem()
        //{
        //    if (string.IsNullOrEmpty(_equippedItemName))
        //        return null;

        //    return _items.FirstOrDefault(item => item.name == _equippedItemName);
        //}

        //public IReadOnlyList<Item> GetAllItems()
        //{
        //    return _items.AsReadOnly();
        //}

        public string GetEquippedItemID() 
            => _equippedItemID;

        public IReadOnlyList<string> GetAllItemIDs() 
            => _itemIDs.AsReadOnly();
    }
}

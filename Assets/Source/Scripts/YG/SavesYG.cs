using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Source.Scripts.Items;

namespace YG
{
    public partial class SavesYG
    {
        private int _points;
		private string _equippedItemName = string.Empty;
		private List<Item> _items = new List<Item>();

		public int Points => _points;

        public void SavePoints(int value)
        {
			if (value < 0)
				throw new ArgumentException("Значение не может быть равным или меньше нуля");

			_points = value;
		}

        public void AddItem(Item item)
        {
            if (item == null)
                return;

            _items = _items.Where(item => item != null).ToList();

            if (_items.Any(existingItem => existingItem.name == item.name))
                return;

            _items.Add(item);
        }

        public void SetEquippedItem(Item item)
        {
            _equippedItemName = item?.name ?? string.Empty;
        }

        public Item GetEquippedItem()
        {
            if (string.IsNullOrEmpty(_equippedItemName))
                return null;

            return _items.FirstOrDefault(item => item.name == _equippedItemName);
        }

        public IReadOnlyList<Item> GetAllItems()
        {
            return _items.AsReadOnly();
        }
    }
}

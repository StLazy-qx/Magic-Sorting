using System;
using System.Collections.Generic;
using System.Linq;
using Items;

namespace YG
{
    public partial class SavesYG
    {
        private int _soundVolume = 75;
        private int _points;
		private string _equippedItemName = string.Empty;
		private List<Item> _items = new List<Item>();

		public int Points => _points;
        public int SoundVolume => _soundVolume;

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

        public List<Item> GetAllItems()
        {
            return new List<Item>(_items);
        }

        public Item GetItem(string name)
        {
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].name == name)
                    return _items[i];
            }

            return null;
        }

        public void SetSoundVolume(int volume)
        {
            ValidateRange(volume, 0, 100, "volume");

            _soundVolume = volume;
        }

        private void ValidateRange(int value, int min, int max, string parameterName)
        {
            if (value < min || value > max)
            {
                throw new ArgumentOutOfRangeException(parameterName,
                    $"{parameterName} должен быть в диапазоне от {min} до {max}");
            }
        }
    }
}

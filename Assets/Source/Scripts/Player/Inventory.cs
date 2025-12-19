using Assets.Source.Scripts.EntryPoint;
using Assets.Source.Scripts.Items;
using Assets.Source.Scripts.Storage;
using System.Collections.Generic;
using System;
using UnityEngine;
using YG;
using System.Linq;

namespace Assets.Source.Scripts.Player
{
    public class Inventory : MonoBehaviour, IObjectInitilizable
    {
        [SerializeField] private Store _store;

        private Item _equippedItem;
        private List<Item> _items = new List<Item>();

        public event Action<Item> ItemEquipped;

        public Item EquippedItem => _equippedItem;
        public bool IsInitialized { get; private set; }

        public void Initialize()
        {
            if (_store == null)
                throw new ArgumentNullException(nameof(_store));

            Load();

            if (_equippedItem != null)
                EquipItem(_equippedItem);

            IsInitialized = true;
        }

        public bool HasItem(Item item)
        {
            ValidateItem(item);

            return _items.Any(currentItem => currentItem.ID == item.ID);
        }

        public void AddItem(Item item)
        {
            ValidateItem(item);

            if (HasItem(item))
                return;

            _items.Add(item);
            YG2.saves.AddItem(item);
        }

        public void EquipItem(Item item)
        {
            ValidateItem(item);

            if (HasItem(item) == false)
                return;

            _equippedItem = item;

            ItemEquipped?.Invoke(item);
            YG2.saves.SaveEquippedItem(item);
        }

        private void Load()
        {
            IReadOnlyList<string> savedIDs = YG2.saves.GetPurchasedItems();

            _items = new List<Item>();

            foreach (string id in savedIDs)
            {
                Item restoredItem = _store.GetItemByID(id);

                if (restoredItem != null)
                    _items.Add(restoredItem);
            }

            string equippedID = YG2.saves.GetEquippedItemID();

            if (string.IsNullOrEmpty(equippedID) == false)
            {
                _equippedItem = _items.FirstOrDefault(
                    item => item.ID == equippedID);
            }
        }

        private void ValidateItem(Item item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
        }
    }
}
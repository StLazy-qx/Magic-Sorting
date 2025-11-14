using System.Collections.Generic;
using System;
using UnityEngine;
using YG;
using System.Linq;
using EntryPoint;
using Items;

namespace PlayerCore
{
    public class Inventory : MonoBehaviour, IObjectInitilizable
    {
        private Item _equippedItem;
        private List<Item> _items = new List<Item>();

        public Item EquippedItem => _equippedItem;
        public bool IsInitialized { get; private set; }

        public event Action<Item> ItemEquipped;

        public void Initilize()
        {
            LoadInventory();

            IsInitialized = true;
        }

        private void LoadInventory()
        {
            _items = YG2.saves.GetAllItems() ?? new List<Item>();

            Item savedEquippedItem = YG2.saves.GetEquippedItem();

            if (savedEquippedItem != null)
                EquipItem(savedEquippedItem);
        }

        public bool HasItem(Item item)
        {
            if (item == null)
                return false;

            return _items.Any(currentItem => currentItem.ID == item.ID);
        }

        public void AddItem(Item item)
        {
            if (item == null)
                return;

            if (HasItem(item))
                return;

            _items.Add(item);
            YG2.saves.AddItem(item);
        }

        public void EquipItem(Item item)
        {
            if (item == null || HasItem(item) == false)
                return;

            _equippedItem = item;

            ItemEquipped?.Invoke(item);
            YG2.saves.SetEquippedItem(item);
        }

        public IReadOnlyList<Item> GetAllItems()
        {
            return _items.AsReadOnly();
        }
    }
}
using System.Collections.Generic;
using System;
using UnityEngine;
using YG;
using System.Linq;
using Assets.Source.Scripts.EntryPoint;
using Assets.Source.Scripts.Items;

namespace Assets.Source.Scripts.Player
{
    public class Inventory : MonoBehaviour, IObjectInitilizable
    {
        private Item _equippedItem;
        private List<Item> _items = new List<Item>();

        public event Action<Item> ItemEquipped;

        public Item EquippedItem => _equippedItem;
        public bool IsInitialized { get; private set; }

        public void Initilize()
        {
            LoadInventory();

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
            YG2.saves.SetEquippedItem(item);
        }

        private void LoadInventory()
        {
            IReadOnlyList<Item> savedItems = YG2.saves.GetAllItems();
            _items = savedItems?.ToList() ?? new List<Item>();
            Item savedEquippedItem = YG2.saves.GetEquippedItem();

            if (savedEquippedItem != null)
                EquipItem(savedEquippedItem);
        }

        private void ValidateItem(Item item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
        }
    }
}
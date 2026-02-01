using Assets.Source.Scripts.EntryPoint;
using Assets.Source.Scripts.Items;
using Assets.Source.Scripts.Storage;
using System.Collections.Generic;
using System;
using UnityEngine;
using YG;
using System.Linq;
using Assets.Source.Scripts.UI.StoreView;

namespace Assets.Source.Scripts.Player
{
    public class Inventory : MonoBehaviour, IObjectInitilizable
    {
        [SerializeField] private Store _store;
        [SerializeField] private ScinSetter _scinSetter;

        private Item _equippedItem;
        private List<Item> _items = new List<Item>();

        public bool IsInitialized { get; private set; }
        public Item EquippedItem => _equippedItem;

        public void Initialize()
        {
            if (_store == null)
                throw new ArgumentNullException(nameof(_store));

            if (_scinSetter == null)
                throw new ArgumentNullException(nameof(_scinSetter));

            Load();

            if (_equippedItem != null)
                EquipItem(_equippedItem);

            IsInitialized = true;
        }

        public bool HasItem(Item item)
        {
            ValidateItem(item);

            return _items.Any(currentItem 
                => currentItem.ID == item.ID);
        }

        public void AddItem(Item item)
        {
            ValidateItem(item);

            if (HasItem(item))
                return;

            _items.Add(item);
            YG2.saves.AddItem(item.ID);
        }

        public void EquipItem(Item item)
        {
            ValidateItem(item);
            item.Equip();

            if (_equippedItem != null)
            {
                _equippedItem.UnEquip();

                ItemView itemView = _equippedItem.GetComponent<ItemView>();

                if(itemView != null)
                    itemView.SetUnselectedState();
            }

            _equippedItem = item;

            _scinSetter.ApplyItem(item);
            YG2.saves.SaveEquippedItem(item.ID);
        }

        private void Load()
        {
            IReadOnlyList<string> savedIDs = YG2.saves.GetPurchasedItems();

            _items = new List<Item>();

            foreach (string id in savedIDs)
            {
                Item item = _store.GetItemByID(id);

                if (item != null)
                    _items.Add(item);

                ItemView itemView = item.GetComponent<ItemView>();

                if (itemView != null)
                    itemView.HideBuyButton();
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
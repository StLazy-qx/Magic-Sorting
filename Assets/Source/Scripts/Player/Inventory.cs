using Assets.Source.Scripts.EntryPoint;
using Assets.Source.Scripts.Items;
using Assets.Source.Scripts.Storage;
using Assets.Source.Scripts.UI.StoreView;
using Assets.Source.Scripts.Extensions;
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
        [SerializeField] private SkinSetter _scinSetter;

        private Item _equippedItem;
        private List<Item> _items = new List<Item>();

        public event Action<NewItemView> ItemBuyed;

        public bool IsEmpty => _items.Count == 0;

        public bool IsInitialized { get; private set; }
        public Item EquippedItem => _equippedItem;

        public void Initialize()
        {
            Guard.NotNull(_store, nameof(_store));
            Guard.NotNull(_scinSetter, nameof(_scinSetter));
            Load();

            if (_equippedItem != null)
                EquipItem(_equippedItem);

            IsInitialized = true;
        }

        public bool HasItem(string itemID)
        {
            Guard.NotNullOrWhiteSpace(itemID, nameof(itemID));

            return _items.Any(item => item.ID == itemID);
        }

        public void AddItem(Item item)
        {
            ValidateItem(item);

            if (HasItem(item.ID))
                return;

            _items.Add(item);
            YG2.saves.AddItem(item.ID);

            NewItemView newItemView = item.GetComponent<NewItemView>();

            if(newItemView != null)
                ItemBuyed?.Invoke(newItemView);
        }

        public void AddItem(string itemID)
        {
            Guard.NotNullOrWhiteSpace(itemID, nameof(itemID));
            Guard.NotNull(_store, nameof(_store));
            YG2.saves.AddItem(itemID);
        }

        public void EquipItem(Item item)
        {
            ValidateItem(item);
            item.Equip();

            Debug.Log("Parameter item in EquipItem" + item != null);

            if (_equippedItem != null)
            {
                Debug.Log("Parameter item in EquipItem" + _equippedItem != null);

                _equippedItem.UnEquip();

                Debug.Log("Equiped _equippedItem in EquipItem");

                NewItemView newItemView = item.GetComponent<NewItemView>();

                Debug.Log("Open newItemView in EquipItem");

                newItemView.ShowPurchaseValidation();

                Debug.Log("newItemView method ShowPurchaseValidation in EquipItem");
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
            }

            string equippedID = YG2.saves.GetEquippedItemID();

            if (string.IsNullOrEmpty(equippedID) == false)
            {
                _equippedItem = _items.FirstOrDefault(
                    item => item.ID == equippedID);
            }
        }

        private void ValidateItem(Item item)
            => Guard.NotNull(item, nameof(item));
    }
}
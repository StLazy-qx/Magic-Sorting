using Assets.Source.Scripts.Items;
using Assets.Source.Scripts.Player;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

namespace Assets.Source.Scripts.Storage
{
    public class Store : MonoBehaviour
    {
        [SerializeField] private List<ItemSO> _itemsData;
        [SerializeField] private PlayerEntity _player;
        [SerializeField] private Inventory _inventory;

        private Wallet _playerWallet;

        public bool IsInitialized { get; private set; }

        private void Awake()
        {
            ValidateInitializeArguments();

            IsInitialized = true;

            ConfirmFirstItem();
        }

        public IReadOnlyList<ItemSO> GetItemsSO()
            => _itemsData.AsReadOnly();

        public void BuyItem(Item selectedItem, Inventory inventory)
        {
            ValidateInventory(selectedItem, inventory);

            if (inventory.HasItem(selectedItem))
                return;

            if (_playerWallet.CanAfford(selectedItem.Price))
            {
                _playerWallet.BuyItem(selectedItem.Price);
                inventory.AddItem(selectedItem);
                selectedItem.Buy();
            }
        }

        public void EquipItem(Item selectedItem, Inventory inventory)
        {
            ValidateInventory(selectedItem, inventory);

            if (inventory.HasItem(selectedItem) == false)
                return;

            inventory.EquipItem(selectedItem);
        }

        public Item GetItemByID(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));

            ItemSO data = _itemsData.FirstOrDefault(item => item.ID == id);
            Item item = Instantiate(data.Item);

            item.Initialize(data);

            return item;
        }

        private void ConfirmFirstItem()
        {
            int indexFirstScin = 0;
            ItemSO firstItemData = _itemsData[indexFirstScin];

            if (firstItemData == null)
            {
                throw new NullReferenceException(
                    "First ItemSO in list is null.");
            }

            if (firstItemData.Item == null)
            {
                throw new NullReferenceException(
                    "Item prefab in ItemSO is missing.");

            }

            Item firstItem = Instantiate(firstItemData.Item);

            firstItem.Initialize(firstItemData);
            _inventory.AddItem(firstItem);
        }

        private void ValidateInitializeArguments()
        {
            if (_player == null)
            {
                throw new NullReferenceException(
                    "Player reference is missing in Store.");
            }

            if (_inventory == null)
            {
                throw new NullReferenceException(
                    "Inventory reference is missing in Store.");
            }

            _playerWallet = _player.Wallet;

            if (_playerWallet == null)
            {
                throw new NullReferenceException(
                    "Player wallet is missing.");
            }

            if (_itemsData == null)
            {
                throw new ArgumentNullException(
                    nameof(_itemsData), "Items list cannot be null.");
            }

            if (_itemsData == null)
            {
                throw new ArgumentNullException(
                    nameof(_itemsData), "Items list cannot be null.");
            }

            if (_itemsData.Count == 0)
            {
                throw new ArgumentException(
                    "Items list cannot be empty.", nameof(_itemsData));
            }

            if (_itemsData.Any(item => item == null))
            {
                throw new ArgumentException(
                    "Items list contains null ItemSO entries.", nameof(_itemsData));
            }
        }

        private void ValidateInventory(Item selectedItem, Inventory inventory)
        {
            if (selectedItem == null)
            {
                throw new ArgumentNullException(nameof(selectedItem),
                    "Item reference cannot be null.");
            }

            if (inventory == null)
            {
                throw new ArgumentNullException(nameof(inventory),
                    "Inventory cannot be null.");
            }

            if (selectedItem.Price < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(selectedItem.Price),
                    "Item price cannot be negative.");
            }
        }
    }
}
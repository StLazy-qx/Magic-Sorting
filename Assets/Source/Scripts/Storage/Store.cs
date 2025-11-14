using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Items;
using PlayerCore;

namespace Storage
{
    public class Store : MonoBehaviour
    {
        [SerializeField] private List<ItemSO> _itemsData;
        [SerializeField] private StoreItemView _itemView;
        [SerializeField] private Player _player;
        [SerializeField] private Inventory _inventory;

        private Wallet _playerWallet;

        public bool IsInitialized { get; private set; }

        private void Awake()
        {
            _playerWallet = _player.Wallet;

            if (_itemsData == null
                || _itemsData.Count == 0
                || _itemsData.Any(item => item == null)
                )
            {
                IsInitialized = false;

                return;
            }

            ConfirmFirstItem();
        }

        public IReadOnlyList<ItemSO> GetItemsSO()
            => _itemsData.AsReadOnly();

        public void PerformBuyItem(Item selectedItem, Inventory inventory)
        {
            if (selectedItem == null)
                return;

            if (inventory.HasItem(selectedItem))
                return;

            if (_playerWallet.CanAfford(selectedItem.Price))
            {
                _playerWallet.BuyItem(selectedItem.Price);
                inventory.AddItem(selectedItem);
                selectedItem.Buy();
            }
        }

        public void PerformEquipItem(Item selectedItem, Inventory inventory)
        {
            if (selectedItem == null)
                return;

            if (inventory == null)
                return;

            if (inventory.HasItem(selectedItem) == false)
                return;

            inventory.EquipItem(selectedItem);
        }

        private void ConfirmFirstItem()
        {
            ItemSO firstItemData = _itemsData[0];

            if (firstItemData != null)
            {
                Item firstItem = Instantiate(firstItemData.Item);

                firstItem.Initialize(firstItemData);
                _inventory.AddItem(firstItem);
                _inventory.EquipItem(firstItem);
            }
        }
    }
}
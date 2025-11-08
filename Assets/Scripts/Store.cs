using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Store : MonoBehaviour, IObjectInitilizable
{
    [SerializeField] private List<ItemSO> _itemsData;
    [SerializeField] private StoreItemView _itemView;
    [SerializeField] private Player _player;

    private Wallet _playerWallet;

    public bool IsInitialized { get; private set; }

    private void Awake()
    {
        _playerWallet = _player.Wallet;
    }

    public void Initilize()
    {
        if (_itemsData == null 
            || _itemsData.Count == 0
            || _itemsData.Any(item => item == null)
            )
        {
            IsInitialized = false;

            return;
        }

        IsInitialized = true;
    }

    public IReadOnlyList<ItemSO> GetItemsSO()
        => _itemsData.AsReadOnly();

    public void TryBuyItem(Item selectedItem, Inventory inventory)
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

    public void TryEquipItem(Item selectedItem, Inventory inventory)
    {
        if (selectedItem == null)
            return;

        if (inventory == null)
            return;

        if (inventory.HasItem(selectedItem) == false)
            return;

        inventory.EquipItem(selectedItem);
    }
}

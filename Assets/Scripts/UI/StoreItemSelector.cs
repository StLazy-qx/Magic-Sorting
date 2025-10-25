using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

//изменить название
public class StoreItemSelector : MonoBehaviour
{
    [SerializeField] private Store _store;
    [SerializeField] private Player _player;
    [SerializeField] private Button _buyButton;
    [SerializeField] private Button _equipButton;
    [SerializeField] private Color _selectItemColor;
    [SerializeField] private Color _buyedItemColor;
    [SerializeField] private Color _defaultColor = Color.white;

    private Wallet _playerWallet;
    private Inventory _inventory;
    private Button _selectedButton;
    private IReadOnlyList<Item> _storeItems;

    private void Awake()
    {
        _playerWallet = _player.Wallet;
        _inventory = _player.Inventory;
        _storeItems = _store.GetItems();
    }

    private void OnEnable()
    {
        //if (_inventory.HasItem(_storeItems[0]) == false)
        //    _inventory.AddItem(_storeItems[0]);

        foreach (Item item in _storeItems)
        {
            Button button = item.View.Button;

            if (button != null)
            {
                Button capturedButton = button;

                capturedButton.onClick.AddListener(()
                    => OnItemSelect(capturedButton));
                UpdateItemVisual(item, capturedButton);
            }
        }

        _buyButton.onClick.AddListener(OnBuyItem);
    }

    private void OnDisable()
    {
        foreach (var item in _storeItems)
        {
            Button button = item.GetComponent<Button>();

            if (button == null)
                continue;

            button.onClick.RemoveAllListeners();
        }

        _buyButton.onClick.RemoveListener(OnBuyItem);
    }

    private void OnItemSelect(Button button)
    {
        if (_selectedButton != null)
            ResetButtonColor(_selectedButton);

        _selectedButton = button;
        _selectedButton.image.color = _selectItemColor;
    }

    private void OnBuyItem()
    {
        //if (_selectedButton == null)
        //    return;

        //Item item = _selectedButton.GetComponent<Item>();

        //if (CanBuyItem(_playerWallet.TotalScore))
        //{
        //    //_player.AddItem(item);
        //    _playerWallet.BuyItem(_storeItems[_selectedButton]);
        //}

        if (_selectedButton == null)
            return;

        Item item = _selectedButton.GetComponent<Item>();

        if (item == null)
            return;

        if (_inventory.HasItem(item))
        {
            Debug.Log("Этот предмет уже куплен.");

            return;
        }

        if (_playerWallet.CanAfford(item.Price))
        {
            _playerWallet.BuyItem(item.Price);
            _inventory.AddItem(item);
            _selectedButton.image.color = _buyedItemColor;
        }
        else
        {
            Debug.Log("Недостаточно средств для покупки.");
        }
    }

    private void UpdateItemVisual(Item item, Button button)
    {
        if (_inventory.HasItem(item))
            button.image.color = _buyedItemColor;
    }

    private void ResetButtonColor(Button button)
    {
        Item item = button.GetComponent<Item>();

        if (item != null && _inventory.HasItem(item))
            button.image.color = _buyedItemColor;
        else
            button.image.color = Color.white;
    }
}

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

//изменить название
public class StoreItemSelector : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Store _store;
    [SerializeField] private Player _player;
    [SerializeField] private Button _buyButton;
    [SerializeField] private Button _equipButton;
    [Header("Colors")]
    [SerializeField] private Color _selectItemColor;
    [SerializeField] private Color _buyedItemColor;
    [SerializeField] private Color _defaultColor = Color.white;
    [Header("UI Links")]
    [SerializeField] private Transform _contentTransform;

    private Wallet _playerWallet;
    private Inventory _inventory;
    private Item _selectedItem;
    private readonly List<Button> _storeItems = new();
    //private IReadOnlyList<Item> _storeItems;

    private void Awake()
    {
        _playerWallet = _player.Wallet;
        _inventory = _player.Inventory;
        //_storeItems = _store.GetItems();
    }


    private void OnEnable()
    {
        if (_contentTransform.childCount == 0)
        {
            Debug.LogWarning("Content Transform пустой! Нет элементов для отображения.");

            return;
        }

        if (_contentTransform.childCount != 0)
        {
            _storeItems.Clear();

            // Получаем все кнопки из контента
            foreach (Transform child in _contentTransform)
            {
                Button button = child.GetComponent<Button>();

                if (button != null)
                {
                    _storeItems.Add(button);
                    //button.onClick.AddListener(() => OnItemSelect(item));
                    button.onClick.AddListener(OnGetText);
                }
            }

            Debug.Log($"StoreItemSelector активирован, найдено предметов: {_storeItems.Count}");
        }

        _buyButton.onClick.AddListener(OnBuyItem);
        _equipButton.onClick.AddListener(OnEquipItem);
    }

    private void OnDisable()
    {
        foreach (var button in _storeItems)
        {
            button.onClick.RemoveAllListeners();
        }

        _buyButton.onClick.RemoveListener(OnBuyItem);
        _equipButton.onClick.RemoveListener(OnEquipItem);
    }

    public void OnGetText()
    {
        Debug.Log("Нажали на кнопку");
    }

    private void OnItemSelect(Item item)
    {
        Debug.Log("Метод выбора предмета [StoreItemSelector] 1");

        item.ChangeBackgroundColor(_selectItemColor);

        Debug.Log("Метод выбора предмета [StoreItemSelector] 2");
    }

    private void OnBuyItem()
    {
        //if (_selectedButton == null)
        //    return;

        //Item item = _selectedButton.GetComponent<Item>();

        //if (item == null)
        //    return;

        //if (_inventory.HasItem(item))
        //{
        //    Debug.Log("Этот предмет уже куплен.");

        //    return;
        //}

        //if (_playerWallet.CanAfford(item.Price))
        //{
        //    _playerWallet.BuyItem(item.Price);
        //    _inventory.AddItem(item);
        //    _selectedButton.image.color = _buyedItemColor;
        //}
        //else
        //{
        //    Debug.Log("Недостаточно средств для покупки.");
        //}

        if (_selectedItem == null)
            return;

        if (_inventory.HasItem(_selectedItem))
        {
            Debug.Log("Этот предмет уже куплен.");
            return;
        }

        if (_playerWallet.CanAfford(_selectedItem.Price))
        {
            _playerWallet.BuyItem(_selectedItem.Price);
            _inventory.AddItem(_selectedItem);
            //_selectedItem.ChangeBackgroundColor(_buyedItemColor);
            _selectedItem.Buy();
        }
        else
        {
            Debug.Log("Недостаточно средств для покупки.");
        }
    }

    private void UpdateItemVisual(Item item)
    {
        //if (_inventory.HasItem(item))
        //    item.ChangeBackgroundColor(_buyedItemColor);
        //else
        //    item.ChangeBackgroundColor(_defaultColor);
    }

    private void ResetItemColor(Item item)
    {
        //if (_inventory.HasItem(item))
        //    item.ChangeBackgroundColor(_buyedItemColor);
        //else
        //    item.ChangeBackgroundColor(_defaultColor);
    }

    private void OnEquipItem()
    {
        if (_selectedItem == null)
        {
            Debug.Log("Не выбран предмет для экипировки.");
            return;
        }

        if (!_inventory.HasItem(_selectedItem))
        {
            Debug.Log("Сначала нужно купить этот предмет.");
            return;
        }

        // Логика экипировки предмета
        _inventory.EquipItem(_selectedItem);
        Debug.Log($"Предмет {_selectedItem.name} экипирован.");
    }

    //private void UpdateItemVisual(Item item, Button button)
    //{
    //    if (_inventory.HasItem(item))
    //        button.image.color = _buyedItemColor;
    //}

    //private void ResetButtonColor(Button button)
    //{
    //    //Item item = button.GetComponent<Item>();

    //    //if (item != null && _inventory.HasItem(item))
    //    //    button.image.color = _buyedItemColor;
    //    //else
    //    //    button.image.color = Color.white;

    //    Item item = button.GetComponent<Item>();

    //    if (item != null)
    //    {
    //        if (_inventory.HasItem(item))
    //            item.ChangeBackgroundColor(_buyedItemColor);
    //        else
    //            item.ChangeBackgroundColor(_defaultColor);
    //    }
    //}
}

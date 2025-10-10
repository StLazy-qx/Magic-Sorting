using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

//изменить название
public class StoreUISelector : MonoBehaviour
{
    [SerializeField] private Item[] _scrollContent;
    [SerializeField] private Color _selectItemColor;
    [SerializeField] private Player _player;
    [SerializeField] private Button _buyButton;

    private Wallet _playerWallet;
    private Button _selectedButton;
    private Dictionary<Button, int> _storeItems = new Dictionary<Button, int>();

    private void Awake()
    {
        _playerWallet = _player.PlayerWallet;

        _storeItems.Clear();

        foreach (Item item in _scrollContent)
        {
            Button itemButton = item.GetComponent<Button>();

            if (itemButton != null)
                _storeItems.Add(itemButton, item.Price);
        }
    }

    private void OnEnable()
    {
        foreach (var item in _storeItems)
        {
            Button button = item.Key;
            int price = item.Value;

            button.onClick.AddListener(() => OnItemsSelect(button));
        }

        _buyButton.onClick.AddListener(OnBuyItem);
    }

    private void OnDisable()
    {
        foreach (var item in _storeItems)
        {
            Button button = item.Key;
            int price = item.Value;

            //изменить вид подписки/отписка от лямбда выражений
            button.onClick.RemoveAllListeners();
        }

        _buyButton.onClick.RemoveListener(OnBuyItem);
    }

    private void OnItemsSelect(Button button)
    {
        //выделять выделенный предмет
        _selectedButton = button;
    }

    private void OnBuyItem()
    {
        if (_selectedButton == null)
            return;

        Item item = _selectedButton.GetComponent<Item>();

        if (CanBuyItem(_playerWallet.TotalScore))
        {
            _player.AddItem(item);
            _playerWallet.BuyItem(_storeItems[_selectedButton]);
        }
    }

    private bool CanBuyItem(int value)
    {
        return value >= _storeItems[_selectedButton];
    }
}

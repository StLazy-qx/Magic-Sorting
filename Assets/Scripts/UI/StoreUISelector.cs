using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

//изменить название
public class StoreUISelector : MonoBehaviour
{
    [SerializeField] private Transform _scrollContent;
    [SerializeField] private TMP_Text _priceItemText;
    [SerializeField] private Color _selectItemColor;
    [SerializeField] private Player _player;
    [SerializeField] private Button _buyButton;

    private Wallet _playerWallet;
    private Button _selectedButton;
    private Dictionary<Button, int> _storeItems = new Dictionary<Button, int>();

    private void Awake()
    {
        int price = 0;
        _playerWallet = _player.PlayerWallet;

        _storeItems.Clear();

        foreach (Transform item in _scrollContent)
        {
            Button itemButton = item.GetComponent<Button>();

            if (itemButton != null)
                _storeItems.Add(itemButton, price);

            price += 300;
        }
    }

    private void OnEnable()
    {
        foreach (var item in _storeItems)
        {
            Button button = item.Key;
            int price = item.Value;

            button.onClick.AddListener(() => OnItemsSelect(button, price));
        }

        _buyButton.onClick.AddListener(OnBuyItem);
    }

    private void OnDisable()
    {
        foreach (var item in _storeItems)
        {
            Button button = item.Key;
            int price = item.Value;

            button.onClick.RemoveListener(() => OnItemsSelect(button, price));
        }

        _buyButton.onClick.RemoveListener(OnBuyItem);
    }

    private void OnItemsSelect(Button button, int price)
    {
        _priceItemText.text = price.ToString();
        _selectedButton = button;
    }

    private void OnBuyItem()
    {
        if (_selectedButton == null)
            return;

        Item item = _selectedButton.GetComponent<Item>();
        Texture texture = item.Texture;

        if (texture != null && CanBuyItem(_playerWallet.TotalScore))
        {
            _player.SetTexture(texture);
            _playerWallet.BuyItem(_storeItems[_selectedButton]);
        }
    }

    private bool CanBuyItem(int value)
    {
        return value >= _storeItems[_selectedButton];
    }
}

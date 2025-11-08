using UnityEngine;
using UnityEngine.UI;

public class SelectItemPresenter : MonoBehaviour
{
    [SerializeField] private StoreItemView _itemView;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private Store _store;
    [SerializeField] private Button _buyButton;
    [SerializeField] private Button _equipButton;

    private Item _selectedItem;

    private void OnEnable()
    {
        _itemView.ItemSelected += OnItemSelected;

        _buyButton.onClick.AddListener(OnBuyButtonClicked);
        _equipButton.onClick.AddListener(OnEquipButtonClicked);
    }

    private void OnDisable()
    {
        _itemView.ItemSelected -= OnItemSelected;

        _buyButton.onClick.RemoveListener(OnBuyButtonClicked);
        _equipButton.onClick.RemoveListener(OnEquipButtonClicked);
    }

    private void OnItemSelected(Button button)
    {
        _selectedItem = button.GetComponent<Item>();

        if (_selectedItem == null)
            return;
    }

    private void OnBuyButtonClicked()
    {
        if (_selectedItem == null)
            return;

        _store.TryBuyItem(_selectedItem, _inventory);
    }

    private void OnEquipButtonClicked()
    {
        if (_selectedItem == null)
            return;

        _store.TryEquipItem(_selectedItem, _inventory);
    }
}

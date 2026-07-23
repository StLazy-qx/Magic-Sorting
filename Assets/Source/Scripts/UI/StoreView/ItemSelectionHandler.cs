using Assets.Source.Scripts.Items;
using Assets.Source.Scripts.Player;
using Assets.Source.Scripts.Storage;
using Assets.Source.Scripts.Extensions;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Assets.Source.Scripts.Pool;

namespace Assets.Source.Scripts.UI.StoreView
{
    public class ItemSelectionHandler : MonoBehaviour
    {
        [SerializeField] private Inventory _inventory;
        [SerializeField] private Store _store;
        [SerializeField] private ItemViewPool _itemViewPool;
        [SerializeField] private Button _buyButton;
        [SerializeField] private Button _equipButton;
        [SerializeField] private Button _selectedButton;
        [SerializeField] private TMP_Text _priceText;

        private Item _selectedItem;
        private NewItemView _currentItemView;

        private void Awake()
        {
            ValidateInitializeArguments();

            _buyButton.gameObject.SetActive(false);
            _selectedButton.gameObject.SetActive(false);
            _equipButton.gameObject.SetActive(true);
        }

        private void Start()
        {
            _itemViewPool.ActivateAll();

            ShowPurchaseValidation();

            if (_inventory.IsInitialized)
                SelectEquippedItem();
            else
                _inventory.Initialized += SelectEquippedItem;
        }

        private void OnEnable()
        {
            _buyButton.onClick.AddListener(OnBuyButtonClicked);
            _equipButton.onClick.AddListener(OnEquipButtonClicked);
            _inventory.ItemBuyed += OnShowEquipButton;
        }

        private void OnDisable()
        {
            _buyButton.onClick.RemoveListener(OnBuyButtonClicked);
            _equipButton.onClick.RemoveListener(OnEquipButtonClicked);
            _inventory.ItemBuyed -= OnShowEquipButton;
        }

        public void OnSelectShowedItem(NewItemView newItemView)
        {
            if (_currentItemView != null && 
                _currentItemView != newItemView)
            {
                _currentItemView.SetDefaultColor();
            }

            _currentItemView = newItemView;
            _selectedItem = _currentItemView.GetComponent<Item>();

            UpdateButtonsState();
            _currentItemView.Selected();
        }

        private void OnBuyButtonClicked()
        {
            if (_selectedItem == null)
                return;

            _store.BuyItem(_selectedItem);

            //NewItemView currentItemView = 
            //    _selectedItem.GetComponent<NewItemView>();
            //currentItemView.ShowPurchaseValidation();
        }

        private void OnEquipButtonClicked()
        {
            if (_selectedItem == null)
                return;

            if (_inventory.HasItem(_selectedItem.ID) == false) 
                return;

            if (_selectedItem == _inventory.EquippedItem)
                return;

            _store.EquipItem(_selectedItem);
            OnSelectShowedItem(_currentItemView);
            UpdateButtonsState();
        }

        private void OnShowEquipButton(NewItemView boughtItemView)
        {
            boughtItemView.ShowPurchaseValidation();

            if (_currentItemView == boughtItemView)
                UpdateButtonsState();

            //if (_currentItemView != null && 
            //    _currentItemView == boughtItemView)
            //{
            //    UpdateButtonsState();
            //}
        }

        private void UpdateButtonsState()
        {
            if (_selectedItem == null)
                return;

            if (_inventory.HasItem(_selectedItem.ID))
            {
                _buyButton.gameObject.SetActive(false);

                bool isSelected = _selectedItem != null &&
                                  _inventory.EquippedItem != null &&
                                  _selectedItem.ID == _inventory.EquippedItem.ID;

                _equipButton.gameObject.SetActive(isSelected == false);
                _selectedButton.gameObject.SetActive(isSelected);

                _selectedButton.interactable = false;
            }
            else
            {
                _buyButton.gameObject.SetActive(true);
                _equipButton.gameObject.SetActive(false);
                _selectedButton.gameObject.SetActive(false);

                _priceText.text = _selectedItem.Price.ToString();
            }
        }

        private void ShowPurchaseValidation()
        {
            foreach (NewItemView view in _itemViewPool.Objects)
            {
                Item item = view.GetComponent<Item>();

                if (item != null && _inventory.HasItem(item.ID))
                    view.ShowPurchaseValidation();
            }
        }

        private void SelectEquippedItem()
        {
            string equippedID = _inventory.EquippedItem?.ID;

            if (string.IsNullOrEmpty(equippedID))
                return;

            foreach (NewItemView view in _itemViewPool.Objects)
            {
                Item item = view.GetComponent<Item>();

                if (item != null && item.ID == equippedID)
                {
                    OnSelectShowedItem(view);

                    break;
                }
            }
        }

        private void ValidateInitializeArguments()
        {
            Guard.NotNull(_inventory, nameof(_inventory));
            Guard.NotNull(_store, nameof(_store));
            Guard.NotNull(_buyButton, nameof(_buyButton));
            Guard.NotNull(_equipButton, nameof(_equipButton));
            Guard.NotNull(_priceText, nameof(_priceText));
        }
    }
}
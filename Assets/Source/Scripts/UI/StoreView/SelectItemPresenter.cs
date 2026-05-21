using Assets.Source.Scripts.Items;
using Assets.Source.Scripts.Player;
using Assets.Source.Scripts.Storage;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Assets.Source.Scripts.UI.StoreView
{
    public class SelectItemPresenter : MonoBehaviour
    {
        [SerializeField] private Inventory _inventory;
        [SerializeField] private Store _store;
        [SerializeField] private Button _buyButton;
        [SerializeField] private Button _equipButton;
        [SerializeField] private TMP_Text _priceText;

        private Item _selectedItem;
        //private ItemView _currentItemView;
        private NewItemView _currentItemView;

        private void Awake()
        {
            ValidateInitializeArguments();

            _buyButton.gameObject.SetActive(false);
            _equipButton.gameObject.SetActive(true);
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

            if (_inventory.HasItem(_selectedItem))
            {
                _buyButton.gameObject.SetActive(false);
                _equipButton.gameObject.SetActive(true);
            }
            else
            {
                _equipButton.gameObject.SetActive(false);
                _buyButton.gameObject.SetActive(true);
                _priceText.text = _selectedItem.Price.ToString();
            }

            //if (_selectedItem.IsBought)
            //{
            //    _buyButton.gameObject.SetActive(false);
            //    _equipButton.gameObject.SetActive(true);
            //}
            //else
            //{
            //    _equipButton.gameObject.SetActive(false);
            //    _buyButton.gameObject.SetActive(true);

            //    _priceText.text = _selectedItem.Price.ToString();
            //}

            _currentItemView.Selected();
        }

        private void OnBuyButtonClicked()
        {
            if (_selectedItem == null)
                return;

            _store.BuyItem(_selectedItem);
        }

        private void OnEquipButtonClicked()
        {
            if (_selectedItem == null)
                return;

            if(_inventory.HasItem(_selectedItem))
                _store.EquipItem(_selectedItem);
        }

        private void OnShowEquipButton(NewItemView boughtItemView)
        {
            if (_currentItemView != null && 
                _currentItemView == boughtItemView)
            {
                _buyButton.gameObject.SetActive(false);
                _equipButton.gameObject.SetActive(true);
            }
        }

        private void ValidateInitializeArguments()
        {
            if (_inventory == null)
            {
                throw new NullReferenceException(
                    "Inventory not assigned in SelectItemPresenter.");
            }

            if (_store == null)
            {
                throw new NullReferenceException(
                    "Store not assigned in SelectItemPresenter.");
            }

            if (_buyButton == null)
            {
                throw new NullReferenceException(
                    "BuyButton not assigned in SelectItemPresenter.");
            }

            if (_equipButton == null)
            {
                throw new NullReferenceException(
                    "EquipButton not assigned in SelectItemPresenter.");
            }
        }
    }
}
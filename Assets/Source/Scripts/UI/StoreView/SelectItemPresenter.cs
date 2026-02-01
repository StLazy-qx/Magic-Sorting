using System;
using UnityEngine;
using UnityEngine.UI;
using Assets.Source.Scripts.Items;
using Assets.Source.Scripts.Player;
using Assets.Source.Scripts.Storage;

namespace Assets.Source.Scripts.UI.StoreView
{
    public class SelectItemPresenter : MonoBehaviour
    {
        [SerializeField] private Inventory _inventory;
        [SerializeField] private Store _store;
        [SerializeField] private Button _buyButton;
        [SerializeField] private Button _equipButton;

        private Item _selectedItem;

        private void Awake()
        {
            ValidateInitializeArguments();
        }

        private void OnEnable()
        {
            _buyButton.onClick.AddListener(OnBuyButtonClicked);
            _equipButton.onClick.AddListener(OnEquipButtonClicked);
        }

        private void OnDisable()
        {
            _buyButton.onClick.RemoveListener(OnBuyButtonClicked);
            _equipButton.onClick.RemoveListener(OnEquipButtonClicked);
        }

        private void OnItemSelected(Button button)
        {
            if (button == null)
                throw new ArgumentNullException(nameof(button));

            _selectedItem = button.GetComponent<Item>();
        }

        private void OnBuyButtonClicked()
        {
            if (_selectedItem == null)
                return;
        }

        private void OnEquipButtonClicked()
        {
            if (_selectedItem == null)
                return;
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
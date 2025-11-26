using Assets.Source.Scripts.Items;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Scripts.UI.StoreView
{
    public class ItemView : MonoBehaviour
    {
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private Image _imageScin;
        [SerializeField] private TMP_Text _priceText;
        [SerializeField] private TMP_Text _boughtText;

        private void Awake()
        {
            ValidateInitializeArguments();

            _boughtText.gameObject.SetActive(false);
        }

        public void Initialize(ItemSO itemData)
        {
            ValidateItemData(itemData);

            _imageScin.sprite = itemData.Icon;
            _priceText.text = itemData.Price.ToString();
        }

        public void ChangeBackgroundColor(Color color)
        {
            _backgroundImage.color = color;
        }

        public void ActivateBoughtText()
        {
            _boughtText.gameObject.SetActive(true);
        }

        private void ValidateInitializeArguments()
        {
            if (_backgroundImage == null)
            {
                throw new NullReferenceException(
                    "Background image reference is missing in ItemView.");
            }

            if (_imageScin == null)
            {
                throw new NullReferenceException(
                    "Skin image reference is missing in ItemView.");
            }

            if (_priceText == null)
            {
                throw new NullReferenceException(
                    "Price text reference is missing in ItemView.");
            }

            if (_boughtText == null)
            {
                throw new NullReferenceException(
                    "Bought text reference is missing in ItemView.");
            }
        }

        private void ValidateItemData(ItemSO itemData)
        {
            if (itemData == null)
            {
                throw new ArgumentNullException(nameof(itemData),
                    "ItemSO data cannot be null when initializing ItemView.");
            }

            if (itemData.Icon == null)
            {
                throw new ArgumentException(
                    "ItemSO Icon is missing.", nameof(itemData));
            }

            if (itemData.Price < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(itemData.Price),
                    "Item price cannot be negative.");
            }
        }
    }
}
using Assets.Source.Scripts.Items;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Scripts.UI.StoreView
{
    public class NewItemView : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private Image _imageScin;
        [SerializeField] private Color _selectedColor;
        [SerializeField] private Color _normalColor;

        private Texture _mainTexture;

        public event Action<NewItemView> OnClicked;
        public event Action<Texture> OnItemSelected;

        private void Awake()
        {
            _button.onClick.AddListener(OnHandleClick);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnHandleClick);
        }

        public void Initialize(ItemSO itemData)
        {
            ValidateItemData(itemData);
            _imageScin.sprite = itemData.Icon;
            _mainTexture = itemData.Skin;
        }

        public void SetSelected(bool selected)
        {
            _backgroundImage.color = selected ? _selectedColor : _normalColor;
        }

        public void Selected()
        {
            _backgroundImage.color = _selectedColor;
        }

        public void SetDefaultColor()
        {
            _backgroundImage.color = _normalColor;
        }

        private void OnHandleClick()
        {
            OnClicked?.Invoke(this);
            OnItemSelected?.Invoke(_mainTexture);
        }

        private void ValidateItemData(ItemSO itemData)
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

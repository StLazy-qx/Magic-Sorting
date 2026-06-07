using Assets.Source.Scripts.Extensions;
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
        [SerializeField] private Image _purchaseValidation;
        [SerializeField] private Color _selectedColor;
        [SerializeField] private Color _normalColor;

        private Texture _mainTexture;

        public event Action<NewItemView> OnClicked;
        public event Action<Texture> OnItemSelected;

        private void Awake()
        {
            _button.onClick.AddListener(OnHandleClick);
            HidePurchaseValidation();
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

        public void ShowPurchaseValidation()
        {
            _purchaseValidation.gameObject.SetActive(true);
        }

        public void HidePurchaseValidation()
        {
            _purchaseValidation.gameObject.SetActive(false);
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
            Guard.NotNull(_backgroundImage, nameof(_backgroundImage));
            Guard.NotNull(_imageScin, nameof(_imageScin));
            Guard.NotNull(_purchaseValidation, nameof(_purchaseValidation));
            Guard.NotNull(itemData, nameof(itemData));
            Guard.NotNull(itemData.Icon, nameof(itemData.Icon));
            Guard.NotNull(itemData.Skin, nameof(itemData.Skin));
            Guard.NotNegative(itemData.Price, nameof(itemData.Price));
        }
    }
}

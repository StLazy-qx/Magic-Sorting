using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Assets.Source.Scripts.Factory;

namespace Assets.Source.Scripts.UI.StoreView
{
    public class StoreItemView : MonoBehaviour
    {
        private List<Button> _createdButtons = new();

        [SerializeField] private StoreItemFactory _itemFactory;
        [SerializeField] private Color _selectItemColor;

        private Color _defaultColor;
        private Button _selectedButton;

        public event Action<Button> ItemSelected;

        private void Awake()
        {
            if (_itemFactory == null)
            {
                throw new NullReferenceException(
                    "StoreItemFactory not assigned in StoreItemView.");
            }
        }

        private void OnEnable()
        {
            if (_itemFactory == null)
                return;

            _itemFactory.Created += OnButtonCreated;
        }

        private void OnDestroy()
        {
            _itemFactory.Created -= OnButtonCreated;
        }

        private void OnButtonCreated(Button button)
        {
            if (button == null)
            {
                throw new ArgumentNullException(nameof(button),
                    "Created button is null in StoreItemView.");
            }

            if (_createdButtons.Contains(button))
                return;

            _defaultColor = button.image.color;

            _createdButtons.Add(button);
            button.onClick.AddListener(() => OnItemSelect(button));
        }

        private void OnItemSelect(Button button)
        {
            if (button == null)
                throw new ArgumentNullException(nameof(button));

            if (_selectedButton != null)
                _selectedButton.image.color = _defaultColor;

            button.image.color = _selectItemColor;
            _selectedButton = button;

            ItemSelected?.Invoke(button);
        }
    }
}
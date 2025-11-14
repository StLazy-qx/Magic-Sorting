using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class StoreItemView : MonoBehaviour
{
    private readonly List<Button> _createdButtons = new();

    [SerializeField] private StoreItemFactory _itemFactory;
    [SerializeField] private Color _selectItemColor;

    private Color _defaultColor;
    private Button _selectedButton;

    public event Action<Button> ItemSelected;

    private void OnEnable()
    {
        if (_itemFactory == null)
            return;

        _itemFactory.Created += OnButtonCreated;
    }

    private void OnDisable()
    {
        _itemFactory.Created -= OnButtonCreated;
    }

    private void OnButtonCreated(Button button)
    {
        if (button == null)
            return;

        _defaultColor = button.image.color;

        _createdButtons.Add(button);
        button.onClick.AddListener(() => OnItemSelect(button));
    }

    private void OnItemSelect(Button button)
    {
        if (_selectedButton != null)
            button.image.color = _defaultColor;

        button.image.color = _selectItemColor;
        _selectedButton = button;

        ItemSelected?.Invoke(button);
    }
}

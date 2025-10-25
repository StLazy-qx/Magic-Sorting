using System;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class Inventory : MonoBehaviour, IObjectInitilizable
{
    private Item _equippedItem;
    private List<Item> _items = new List<Item>();

    public Item EquippedItem => _equippedItem;

    public event Action<Item> ItemEquipped;
    public event Action<Item> ItemAdded;

    public bool IsInitialized { get; private set; }

    public void Initilize()
    {
        LoadInventory();

        IsInitialized = true;
    }

    private void LoadInventory()
    {
        _items = YG2.saves.GetAllItems() ?? new List<Item>();
        Debug.Log($"Загружено предметов в инвентарь: {_items.Count}");

        Item savedEquippedItem = YG2.saves.GetEquippedItem();

        if (savedEquippedItem != null)
        {
            EquipItem(savedEquippedItem);
            Debug.Log($"Загружен и экипирован предмет: {savedEquippedItem.name}");
        }

        Debug.Log($"Загружено предметов в инвентарь: {_items.Count}");
    }

    public bool HasItem(Item item)
    {
        if (item == null)
            return false;

        return _items.Contains(item);
    }

    public void AddItem(Item item)
    {
        if (item == null) 
            return;

        if (HasItem(item))
            return;

        _items.Add(item);
        YG2.saves.AddItem(item);

        Debug.Log($"Предмет {item.name} добавлен в инвентарь.");
        Debug.Log("Количество предметов в инветоре " + _items.Count);

        ItemAdded?.Invoke(item);
    }

    public void EquipItem(Item item)
    {
        if (item == null)
        {
            Debug.LogWarning("Нельзя экипировать null-предмет.");

            return;
        }

        if (!HasItem(item))
        {
            Debug.LogWarning($"Предмет {item.name} не найден " +
                $"в инвентаре, экипировка невозможна.");

            return;
        }

        _equippedItem = item;

        YG2.saves.SetEquippedItem(item);
        ItemEquipped?.Invoke(item);
    }

    public IReadOnlyList<Item> GetAllItems()
    {
        return _items.AsReadOnly();
    }
}

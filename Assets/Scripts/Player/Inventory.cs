using System.Collections.Generic;
using UnityEngine;
using System;
using YG;
using System.Linq;

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

        Item savedEquippedItem = YG2.saves.GetEquippedItem();

        if (savedEquippedItem != null)
            EquipItem(savedEquippedItem);
    }

    public bool HasItem(Item item)
    {
        if (item == null)
            return false;

        return _items.Any(i => i.ID == item.ID);
    }

    public void AddItem(Item item)
    {
        if (item == null) 
            return;

        if (HasItem(item))
            return;

        _items.Add(item);
        YG2.saves.AddItem(item);
        ItemAdded?.Invoke(item);
    }

    public void EquipItem(Item item)
    {
        if (item == null || HasItem(item) == false)
            return;

        _equippedItem = item;

        YG2.saves.SetEquippedItem(item);
        ItemEquipped?.Invoke(item);
    }

    public IReadOnlyList<Item> GetAllItems()
    {
        return _items.AsReadOnly();
    }
}

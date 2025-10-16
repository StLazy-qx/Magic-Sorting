using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<Item> _items = new List<Item>();

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

        Debug.Log($"Предмет {item.name} добавлен в инвентарь.");
        Debug.Log("Количество предметов в инветоре " + _items.Count);
    }

    public IReadOnlyList<Item> GetAllItems()
    {
        return _items.AsReadOnly();
    }
}

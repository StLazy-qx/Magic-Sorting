using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Store : MonoBehaviour
{
    [SerializeField] private List<Item> _items;

    public bool IsInitialize { get; private set; }

    private void Start()
    {
        Debug.Log("Количество предметов в ММАГАЗИНЕ " + _items.Count);
    }

    public void Initialize()
    {

        if (_items != null && _items.All(item => item != null))
        {
            IsInitialize = true;
        }
        else
        {
            IsInitialize = false;

            Debug.LogWarning("Store initialization failed: some items are null or list is empty.");
        }
    }

    public IReadOnlyList<Item> GetAllItems()
        =>_items.AsReadOnly();
}

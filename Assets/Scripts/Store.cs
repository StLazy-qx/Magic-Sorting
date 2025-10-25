using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Store : MonoBehaviour
{
    private readonly List<Item> _items = new List<Item>();

    [SerializeField] private List<ItemSO> _itemsData;

    public bool IsInitialize { get; private set; }

    private void Awake()
    {
        //дальше через EmptryPoint
        Initialize();
    }

    public void Initialize()
    {
        _items.Clear();

        //if (_itemsData != null && 
        //    _itemsData.Count > 0 && 
        //    _itemsData.All(item => item != null))
        //{
        //    IsInitialize = true;
        //}
        //else
        //{
        //    IsInitialize = false;
        //}

        if (_itemsData == null || _itemsData.Count == 0)
        {
            IsInitialize = false;

            return;
        }

        foreach (ItemSO data in _itemsData)
        {
            if (data == null || data.Item == null)
            {
                Debug.LogError($"Store: отсутствует ссылка на Item в {data?.name}");

                continue;
            }

            Item newItem = Instantiate(data.Item, transform);
            newItem.Initialize(data);
            _items.Add(newItem);
        }

        IsInitialize = _items.Count > 0;
    }

    public IReadOnlyList<Item> GetItems()
        => _items.AsReadOnly();

    public IReadOnlyList<ItemSO> GetItemsSO()
    => _itemsData.AsReadOnly();
}

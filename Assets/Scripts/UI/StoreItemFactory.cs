using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreItemFactory : Factory<Button>
{
    [SerializeField] private Store _store;
    [SerializeField] private Player _player;
    [SerializeField] private Transform _contentTransform;

    public event Action<Button> Created;

    protected override void BuildObjects()
    {
        ClearList();

        IReadOnlyList<ItemSO> items = _store.GetItemsSO();

        if (items == null || items.Count == 0)
            return;

        foreach (ItemSO itemData in items)
        {
            Button button = Instantiate(Prefab, _contentTransform);

            Add(button);
            Created?.Invoke(button);

            Item itemComponent = button.GetComponent<Item>();

            if (itemComponent == null)
            {
                itemComponent = button.gameObject.AddComponent<Item>();
            }

            itemComponent.Initialize(itemData);
        }

        NotifyObjectsChanged();
    }
}

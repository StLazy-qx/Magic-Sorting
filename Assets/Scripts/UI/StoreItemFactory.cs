using System;
using UnityEngine;
using UnityEngine.UI;

public class StoreItemFactory : Factory<Button>
{
    [Header("References")]
    [SerializeField] private Store _store;
    [SerializeField] private Player _player;
    [SerializeField] private Transform _contentTransform;

    public event Action<Button> Created;

    protected override void BuildObjects()
    {
        ClearList();

        var items = _store.GetItemsSO();

        if (items == null || items.Count == 0)
            return;

        foreach (ItemSO itemData in items)
        {
            Button newButton = Instantiate(Prefab, _contentTransform);

            Add(newButton);
            Created?.Invoke(newButton);

            Item itemComponent = newButton.GetComponent<Item>();

            if (itemComponent == null)
            {
                itemComponent = newButton.gameObject.AddComponent<Item>();
            }

            itemComponent.Initialize(itemData);
        }

        NotifyObjectsChanged();
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayerCore;
using Items;
using Storage;

namespace FactoryCore
{
    public class StoreItemFactory : Factory<Button>
    {
        [SerializeField] private Store _store;
        [SerializeField] private Player _player;
        [SerializeField] private Transform _contentTransform;

        public event Action<Button> Created;

        public void SetContentTransform(Transform contentTransform)
        {
            _contentTransform = contentTransform;
        }

        protected override void BuildObjects()
        {
            ValidateBuildRequirements();
            ClearList();

            IReadOnlyList<ItemSO> items = _store.GetItemsSO();

            foreach (ItemSO itemData in items)
            {
                Button button = Instantiate(Prefab, _contentTransform);

                Add(button);
                Created?.Invoke(button);

                Item itemComponent = button.GetComponent<Item>();

                if (itemComponent == null)
                    itemComponent = button.gameObject.AddComponent<Item>();

                itemComponent.Initialize(itemData);
            }

            NotifyObjectsChanged();
        }

        private void ValidateBuildRequirements()
        {
            if (_store == null)
                throw new ArgumentNullException(nameof(_store));

            if (_contentTransform == null)
                throw new ArgumentNullException(nameof(_contentTransform));
        }
    }
}
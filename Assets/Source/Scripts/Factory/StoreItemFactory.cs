using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Source.Scripts.Player;
using Assets.Source.Scripts.Items;
using Assets.Source.Scripts.Storage;
using Assets.Source.Scripts.UI.StoreView;

namespace Assets.Source.Scripts.Factory
{
    public class StoreItemFactory : Factory<Button>
    {
        [SerializeField] private Store _store;
        [SerializeField] private SkinSetter _modelSkin;

        private SelectItemPresenter _itemPresenter;
        private Transform _contentTransform;

        public event Action<Button> Created;

        public void Initialize(Transform contentTransform, SelectItemPresenter itemPresenter)
        {
            _itemPresenter = itemPresenter;
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
                Item item = button.GetComponent<Item>();
                NewItemView itemView = button.GetComponent<NewItemView>();
                OnPushItemPresenter(itemView);

                if (item == null)
                    throw new ArgumentNullException(nameof(item));

                if (itemView == null)
                    throw new ArgumentNullException(nameof(itemView));

                item.Initialize(itemData);
                itemView.Initialize(itemData);
                Add(button);
                Created?.Invoke(button);
            }

            NotifyObjectsChanged();
        }

        private void OnPushItemPresenter(NewItemView newItemView)
        {
            newItemView.OnClicked += _itemPresenter.OnSelectShowedItem;
            newItemView.OnItemSelected += _modelSkin.OnShowItemTexture;
        }

        private void ValidateBuildRequirements()
        {
            if (_store == null)
                throw new ArgumentNullException(nameof(_store));

            if (Prefab == null)
                throw new ArgumentNullException(nameof(Prefab));

            if (_itemPresenter == null)
                throw new ArgumentNullException(nameof(_itemPresenter));

            if (_contentTransform == null)
                throw new ArgumentNullException(nameof(_contentTransform));
        }
    }
}
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
        //[SerializeField] private Inventory _inventory;
        [SerializeField] private SkinSetter _modelSkin;

        //назначить через платформ адаптер
        [SerializeField] private SelectItemPresenter _itemPresenter;

        private Transform _contentTransform;

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
                //StoreItemPresenter presenter = button.GetComponent<StoreItemPresenter>();
                Item item = button.GetComponent<Item>();
                //ItemView itemView = button.GetComponent<ItemView>();
                NewItemView itemView = button.GetComponent<NewItemView>();
                OnPushItemPresenter(itemView);

                //if (presenter == null)
                //    throw new ArgumentNullException(nameof(presenter));

                if (item == null)
                    throw new ArgumentNullException(nameof(item));

                if (itemView == null)
                    throw new ArgumentNullException(nameof(itemView));

                item.Initialize(itemData);
                itemView.Initialize(itemData);
                //presenter.Initialize(item, _store);
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

            //if (_inventory == null)
            //    throw new ArgumentNullException(nameof(_inventory));

            if (Prefab == null)
                throw new ArgumentNullException(nameof(Prefab));

            if (_contentTransform == null)
                throw new ArgumentNullException(nameof(_contentTransform));
        }
    }
}
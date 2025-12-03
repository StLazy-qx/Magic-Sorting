using Assets.Source.Scripts.UI.StoreView;
using System;
using UnityEngine;
using YG;

namespace Assets.Source.Scripts.Items
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private ItemView _itemView;

        private string _id;
        private Texture _texture;
        private int _price;

        public string ID => _id;
        public int Price => _price;
        public Texture Texture => _texture;
        public bool IsBuyed { get; private set; }

        private void Start()
        {
            if (YG2.saves.HasItem(_id))
                ActivateBought();
        }

        public void Initialize(ItemSO itemData)
        {
            ValidateObjects(itemData);
            _itemView.Initialize(itemData);

            _id = itemData.ID;
            _price = itemData.Price;
            _texture = itemData.Scin;
        }

        public void Buy()
        {
            if (IsBuyed || _itemView == null)
                return;

            IsBuyed = true;

            _itemView.ActivateBoughtText();
        }

        private void ActivateBought()
        {
            if (_itemView == null)
                return;

            _itemView.ActivateBoughtText();
        }

        private void ValidateObjects(ItemSO itemData)
        {
            if (_itemView == null)
                throw new ArgumentNullException(nameof(_itemView));

            if (itemData.Price < 0)
            {
                throw new ArgumentException(
                    "Price must be positive", nameof(itemData));
            }

            if (string.IsNullOrWhiteSpace(itemData.ID))
            {
                throw new ArgumentException(
                    "ID cannot be null or empty", nameof(itemData));
            }
        }
    }
}
using UnityEngine;
using Items;

namespace PlayerCore
{
    public class ScinSetter : MonoBehaviour
    {
        [SerializeField] private SkinnedMeshRenderer _meshRenderer;
        [SerializeField] private Inventory _inventory;

        private Material _materialInstance;

        private void Start()
        {
            if (_inventory.IsInitialized && _inventory.EquippedItem != null)
                OnEquipItem(_inventory.EquippedItem);
        }

        private void OnEnable()
        {
            _inventory.ItemEquipped += OnEquipItem;
        }

        private void OnDisable()
        {
            _inventory.ItemEquipped -= OnEquipItem;
        }

        private void OnEquipItem(Item item)
        {
            if (item == null)
                return;

            if (_inventory.HasItem(item) == false)
                return;

            if (_materialInstance == null && _meshRenderer != null)
            {
                _materialInstance = _meshRenderer.material;
            }

            ApplyItemTexture(item);
        }

        private void ApplyItemTexture(Item item)
        {
            if (_materialInstance == null || item.Texture == null)
                return;

            _materialInstance.SetTexture("_MainTex", item.Texture);
        }

        private void InitializeMaterial()
        {
            if (_meshRenderer != null)
                _materialInstance = _meshRenderer.material;
        }
    }
}
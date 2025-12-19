using Assets.Source.Scripts.Items;
using UnityEngine;

namespace Assets.Source.Scripts.Player
{
    public class ScinSetter : MonoBehaviour
    {
        [SerializeField] private SkinnedMeshRenderer _meshRenderer;
        [SerializeField] private Inventory _inventory;

        private Material _materialInstance;

        private void Start()
        {
            if (_meshRenderer == null)
            {
                throw new System.ArgumentNullException(nameof(_meshRenderer),
                    "SkinnedMeshRenderer must be assigned");
            }

            if (_inventory == null)
            {
                throw new System.ArgumentNullException(nameof(_inventory),
                    "Inventory must be assigned");
            }

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

            if (_materialInstance == null)
                InitializeMaterial();

            ApplyItemTexture(item);
        }

        private void ApplyItemTexture(Item item)
        {
            if (_materialInstance == null)
            {
                throw new System.InvalidOperationException(
                    "Material instance is not initialized");
            }

            if (item.Texture == null)
                return;

            _materialInstance.SetTexture("_MainTex", item.Texture);
        }

        private void InitializeMaterial()
        {
            if (_meshRenderer == null)
            {
                throw new System.InvalidOperationException(
                    "MeshRenderer is not assigned");
            }

            _materialInstance = _meshRenderer.material;
        }
    }
}
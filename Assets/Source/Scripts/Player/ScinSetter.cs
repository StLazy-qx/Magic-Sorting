using Assets.Source.Scripts.Items;
using UnityEngine;

namespace Assets.Source.Scripts.Player
{
    public class ScinSetter : MonoBehaviour
    {
        [SerializeField] private SkinnedMeshRenderer _meshRenderer;

        private Material _materialInstance;

        private void Start()
        {
            if (_meshRenderer == null)
            {
                throw new System.ArgumentNullException(nameof(_meshRenderer),
                    "SkinnedMeshRenderer must be assigned");
            }

            _materialInstance = _meshRenderer.material;
        }

        public void ApplyItem(Item item)
        {
            if (item == null || item.Texture == null)
                return;

            if (_materialInstance == null)
            {
                throw new System.InvalidOperationException(
                    "Material instance is not initialized");
            }

            _materialInstance.SetTexture("_MainTex", item.Texture);
        }

        // может убрать
        public void Clear()
        {
            _materialInstance.SetTexture("_MainTex", null);
        }
    }
}
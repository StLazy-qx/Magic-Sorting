using UnityEngine;
using Assets.Source.Scripts.MagicCells;

namespace Assets.Source.Scripts.Factory
{
    public class MagicCellsFactory : Factory<MagicCell>
    {
        public MagicCell CreateCell(Transform parent, 
            Vector3 localPosition, 
            Color color)
        {
            MagicCell cell = Instantiate(Prefab, parent);
            cell.transform.localPosition = localPosition;

            cell.SetColor(color);
            Add(cell);
            NotifyObjectsChanged();

            return cell;
        }

        public float GetCellHeight()
        {
            float cellHeight = 0f;

            Renderer renderer = Prefab.GetComponentInChildren<Renderer>();

            return renderer != null ? 
                renderer.bounds.size.y : cellHeight;
        }

        protected override void BuildObjects() { }
    }
}
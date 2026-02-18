using Assets.Source.Scripts.InteractiveObjects;
using System;
using System.Collections.Generic;

namespace Assets.Source.Scripts.Pool
{
    class MagicColumnPool : Pool<MagicColumn>
    {
        public event Action<IReadOnlyList<MagicColumn>> ActiveColumnsChanged;

        public override MagicColumn Activate()
        {
            MagicColumn column = base.Activate();

            if (column != null)
                NotifyStateChanged();

            return column;
        }

        public override void Deactivate(MagicColumn obj)
        {
            base.Deactivate(obj);
            NotifyStateChanged();
        }

        public void DeactivateAll()
        {
            for (int i = 0; i < _objects.Count; i++)
            {
                if (_objects[i].gameObject.activeSelf)
                    Deactivate(_objects[i]);
            }
        }

        public bool TryGetActive(int index, out MagicColumn column)
        {
            var active = GetActiveObjects();

            if (index < 0 || index >= active.Count)
            {
                column = null;

                return false;
            }

            column = active[index];

            return true;
        }

        private void NotifyStateChanged()
        {
            ActiveColumnsChanged?.Invoke(GetActiveObjects());
        }
    }
}

using System.Collections.Generic;
using Assets.Source.Scripts.Colorize;
using UnityEngine;

namespace Assets.Source.Scripts.Pool
{
    public class EntryListColorPool : MonoBehaviour
    {
        private readonly List<EntryListColor> _pool = new List<EntryListColor>();

        public int Count => _pool.Count;

        public void Add(EntryListColor entry)
        {
            if (entry == null)
                return;

            _pool.Add(entry);
        }

        public EntryListColor Get()
        {
            if (_pool.Count == 0)
                return null;

            List<int> checkedIndexes = new List<int>();

            while (checkedIndexes.Count < _pool.Count)
            {
                int randomIndex = Random.Range(0, _pool.Count);

                if (checkedIndexes.Contains(randomIndex))
                    continue;

                checkedIndexes.Add(randomIndex);

                EntryListColor entry = _pool[randomIndex];

                if (entry.IsEmpty() == false)
                    return entry;
            }

            return null;
        }

        public void Clear()
        {
            _pool.Clear();
        }

        private EntryListColor GetRandom()
        {
            int randomIndex = Random.Range(0, _pool.Count);

            return _pool[randomIndex];
        }
    }
}

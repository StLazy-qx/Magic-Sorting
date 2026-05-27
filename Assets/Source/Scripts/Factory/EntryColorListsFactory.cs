using Assets.Source.Scripts.Pool;
using Assets.Source.Scripts.Colorize;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Source.Scripts.Factory
{
    public class EntryColorListsFactory : MonoBehaviour
    {
        [SerializeField] private int _countOneColor;
        [SerializeField] private EntryListColorPool _pool;

        public void Initialize(IReadOnlyList<Color> colors)
        {
            _pool.Clear();

            foreach (Color color in colors)
            {
                EntryListColor entryList = new EntryListColor(color, _countOneColor);

                _pool.Add(entryList);
            }
        }

        public void Reset()
        {
            _pool.Clear();
        }
    }
}

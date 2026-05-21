using Assets.Source.Scripts.Colorize;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Source.Scripts.Factory
{
    public class EntryColorListsFactory : MonoBehaviour
    {
        [SerializeField] private int CountOneColor;

        private List<EntryListColor> _pools = new List<EntryListColor>();

        public void Initialize(IReadOnlyList<Color> colors)
        {
            IReadOnlyList<Color> palette = colors;

            _pools.Clear();

            foreach (Color color in palette)
            {
                EntryListColor entryList = new EntryListColor(color, CountOneColor);

                _pools.Add(entryList);
            }
        }
    }
}

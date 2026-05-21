using System.Collections.Generic;
using UnityEngine;

namespace Assets.Source.Scripts.Colorize
{
    public class EntryListColor
    {
        private Color _mainColor;
        private List<Color> _colors;

        public EntryListColor(Color initialColor, int colorCount)
        {
            _colors = new List<Color>(colorCount);

            for (int i = 0; i < colorCount; i++)
            {
                _colors.Add(initialColor);
            }

            Debug.Log($"Создан EntryListColor, цвет {initialColor}, количество {_colors.Count}");
        }

        public Color CurrentColor => _mainColor;

        public Color TakeColor()
        {
            Color color = _colors[0];

            _colors.RemoveAt(0);

            return color;
        }

        public bool IsNonEmpty()
        {
            return _colors.Count > 0;
        }

        public void Reset()
        {

        }
    }
}

using UnityEngine;

namespace Colorize
{
    public interface IColorable
    {
        public Color Color { get; }

        public void SetColor(Color color);
    }

}
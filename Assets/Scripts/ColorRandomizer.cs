using System;
using UnityEngine;

public class ColorRandomizer : MonoBehaviour
{
    private const float AlphaValue = 0.93f;
    private EnumColor[] _allColors = (EnumColor[])Enum.GetValues(typeof(EnumColor));
    private Color[] _currentColors;

    public Color GenerateRandomColor()
    {
        if (_currentColors == null || _currentColors.Length == 0)
        {
            Debug.LogWarning("Массив _currentColors пуст или не инициализирован. Возврат ко всем цветам.");

            return TransformEnumToColor(_allColors[UnityEngine.Random.Range(0, _allColors.Length)]);
        }

        return _currentColors[UnityEngine.Random.Range(0, _currentColors.Length)];
    }

    public Color[] CrateArrayColors(int colorsNumber)
    {
        if (colorsNumber > _allColors.Length)
        {
            Debug.LogWarning($"Запрошено {colorsNumber} цветов, " +
                             $"но доступно только {_allColors.Length}. Установим максимум.");

            colorsNumber = _allColors.Length;
        }

        int[] shuffledIndices = ShuffleIndices(_allColors.Length);

        _currentColors = new Color[colorsNumber];

        for (int i = 0; i < colorsNumber; i++)
        {
            _currentColors[i] = TransformEnumToColor(_allColors[shuffledIndices[i]]);
        }

        return _currentColors;
    }

    private int[] ShuffleIndices(int length)
    {
        int[] indices = new int[length];

        for (int i = 0; i < length; i++)
        {
            indices[i] = i;
        }

        for (int i = length - 1; i > 0; i--)
        {
            int index = UnityEngine.Random.Range(0, i + 1);

            (indices[i], indices[index]) = (indices[index], indices[i]);
        }

        return indices;
    }

    private Color TransformEnumToColor(EnumColor randomColor)
    {
        switch (randomColor)
        {
            case EnumColor.Red:
                return new Color(1f, 0f, 0f, AlphaValue);

            case EnumColor.Green:
                return new Color(0f, 1f, 0f, AlphaValue);

            case EnumColor.Blue:
                return new Color(0f, 0f, 1f, AlphaValue);

            case EnumColor.Yellow:
                return new Color(1f, 1f, 0f, AlphaValue);

            case EnumColor.Orange:
                return new Color(1f, 0.5f, 0f, AlphaValue);

            case EnumColor.Purple:
                return new Color(0.5f, 0f, 0.5f, AlphaValue);

            default:
                return new Color(1f, 1f, 1f, AlphaValue);
        }
    }
}
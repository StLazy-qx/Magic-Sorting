using System;
using UnityEngine;

public class ColorRandomizer : MonoBehaviour
{
    private const float AlphaValue = 0.93f;

    public Color[] CreateOriginalArrayColor(int number)
    {
        EnumColor[] allColors = (EnumColor[])Enum.GetValues(typeof(EnumColor));

        if (number > allColors.Length)
        {
            Debug.LogWarning($"Запрошено {number} уникальных цветов, " +
                $"но доступно только {allColors.Length}. Вернём максимум.");

            number = allColors.Length;
        }

        EnumColor[] shuffledColors = (EnumColor[])allColors.Clone();

        for (int i = shuffledColors.Length - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            (shuffledColors[i], shuffledColors[j]) = (shuffledColors[j], shuffledColors[i]);
        }

        Color[] result = new Color[number];

        for (int i = 0; i < number; i++)
        {
            result[i] = TransformEnumToColor(shuffledColors[i]);
        }

        return result;
    }

    public Color GenerateRandomColor()
    {
        EnumColor[] colorValues = (EnumColor[])Enum.GetValues(typeof(EnumColor));
        EnumColor randomColor = colorValues[UnityEngine.Random.Range(0, colorValues.Length)];

        return TransformEnumToColor(randomColor);
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

            default:
                return new Color(1f, 1f, 1f, AlphaValue);
        }
    }
}

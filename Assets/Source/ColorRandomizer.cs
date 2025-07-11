using System;
using UnityEngine;

[RequireComponent(typeof(Renderer))]

public class ColorRandomizer : MonoBehaviour
{
    private const float AlphaValue = 0.93f;

    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _renderer.material.color = GenerateRandomColor();
    }

    private Color GenerateRandomColor()
    {
        EnumColor[] colorValues = (EnumColor[])Enum.GetValues(typeof(EnumColor));
        EnumColor randomColor = colorValues[UnityEngine.Random.Range(0, colorValues.Length)];

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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorMarker : MonoBehaviour
{
    private Color _assignedColor;

    public Color AssignedColor => _assignedColor;

    public void Init(IColorable colorable)
    {
        if (colorable == null)
        {
            throw new ArgumentNullException(nameof(colorable), 
                "Colorable объект не может быть null");
        }

        _assignedColor = colorable.Color;
    }

    public Color GetColor()
    {
        return _assignedColor;
    }
}

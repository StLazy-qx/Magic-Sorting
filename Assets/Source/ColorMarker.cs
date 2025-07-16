using System;
using UnityEngine;

public class ColorMarker : MonoBehaviour
{
    [SerializeField] private Vessel _vessel;
    [SerializeField] private Flag _flag;

    private Liquid _liquid;

    private Color _assignedColor;

    public Color AssignedColor => _assignedColor;

    public void Init(Color color)
    {
        if (_vessel != null)
            _liquid = _vessel.Liquid;

        _assignedColor = color;

        if (_vessel != null)
            _vessel.SetColor(color);

        if (_liquid != null) 
            _liquid.SetColor(color);

        if (_flag != null) 
            _flag.SetColor(color);
    }
}

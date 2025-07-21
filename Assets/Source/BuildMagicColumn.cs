using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMagicColumn : MonoBehaviour
{
    private IReadOnlyList<Vessel> _vessels;
    private List<Color> _colors = new List<Color>();
    private Queue<Color> _mixedColors = new Queue<Color>();

    public bool IsInitialized { get; private set; }
    public int TotalColors => _colors.Count;

    public void AcceptVesselsList(IReadOnlyList<Vessel> vessels)
    {
        if (vessels == null)
            throw new ArgumentNullException(nameof(vessels), "Vessels list cannot be null");

        if (vessels.Count == 0)
            throw new ArgumentException("Vessels list cannot be empty", nameof(vessels));

        foreach (var vessel in vessels)
        {
            if (vessel == null)
                throw new ArgumentException("Vessels list contains null elements", nameof(vessels));
        }

        _vessels = vessels;

        GenerateColorList();
        ShuffleColors();

        IsInitialized = true;
    }

    public Color? GetRandomColor()
    {
        if (_mixedColors.Count > 0)
        {
            return _mixedColors.Dequeue();
        }

        return null;
    }

    private void GenerateColorList()
    {
        _colors.Clear();

        foreach (Vessel vessel in _vessels)
        {
            for (int i = 0; i < vessel.Count; i++)
            {
                _colors.Add(vessel.Color);
            }
        }
    }

    private void ShuffleColors()
    {
        for (int i = _colors.Count - 1; i > 0; i--)
        {
            int randomNumber = UnityEngine.Random.Range(0, i + 1);

            Color tempColor = _colors[i];
            _colors[i] = _colors[randomNumber];
            _colors[randomNumber] = tempColor;
        }

        _mixedColors.Clear();

        foreach (Color color in _colors)
        {
            _mixedColors.Enqueue(color);
        }
    }
}

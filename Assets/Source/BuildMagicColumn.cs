using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMagicColumn : MonoBehaviour
{
    private IReadOnlyList<Vessel> _vessels;
    private List<Color> _colors = new List<Color>();
    private Queue<Color> _mixedColors = new Queue<Color>();

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
        int n = _colors.Count;

        for (int i = 0; i < n - 1; i++)
        {
            int r = UnityEngine.Random.Range(i, n);
            Color temp = _colors[i];
            _colors[i] = _colors[r];
            _colors[r] = temp;
        }

        _mixedColors.Clear();

        foreach (Color color in _colors)
        {
            _mixedColors.Enqueue(color);
        }
    }
}

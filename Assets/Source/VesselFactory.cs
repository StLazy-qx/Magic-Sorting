using System;
using System.Collections.Generic;
using UnityEngine;

public class VesselFactory : MonoBehaviour
{
    [SerializeField] private Vessel _prefab;
    [SerializeField] private DistributerMagicCell _distributerMagicCell;
    [SerializeField] private Transform[] _points;

    private ColorRandomizer _colorRandomizer;
    private List<Vessel> _vessels;

    public IReadOnlyList<Vessel> Vessels => _vessels;

    private void Awake()
    {
        _colorRandomizer = GetComponent<ColorRandomizer>();
        _vessels = new List<Vessel>();
    }

    private void Start()
    {
        Create();
    }

    //по другому назвать метод
    public void Create()
    {
        for (int i = 0; i < _points.Length; i++)
        {
            Vessel vessel = Instantiate(
                _prefab,
                _points[i].position,
                Quaternion.identity);

            _vessels.Add(vessel);
        }

        _distributerMagicCell.AcceptVesselsList(Vessels);
        SetColor();
    }

    private void SetColor()
    {
        Color[] colors = _colorRandomizer.CreateOriginalArrayColor(_points.Length);

        int j = 0;

        foreach (var vessel in _vessels)
        {
            var marker = vessel.GetComponent<ColorMarker>();
            marker.Init(colors[j]);

            j++;
        }
    }
}

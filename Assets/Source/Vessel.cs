using System;
using UnityEngine;

[RequireComponent(typeof(VesselAggregator))]

public class Vessel : MonoBehaviour, IColorable
{
    [SerializeField] private Liquid _liquid;
    [SerializeField] private int _maxSize;

    private Color _mainColor;
    private VesselAggregator _aggregator;

    public int Count => _maxSize;
    public Color Color => _mainColor;
    public Liquid Liquid => _liquid;

    public event Action Completed;

    private void Awake()
    {
        _aggregator = GetComponent<VesselAggregator>();
    }

    public void TakeMagic(MagicCell cell)
    {
        if (cell == null)
            return;

        if (_aggregator.IsFull)
            Completed?.Invoke();

        _aggregator.GrowUpVolume();
    }

    public void SetColor(Color color)
    {
        _mainColor = color;
    }
}

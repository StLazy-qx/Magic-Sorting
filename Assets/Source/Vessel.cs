using System;
using UnityEngine;

[RequireComponent(typeof(VesselAggregator))]

public class Vessel : MonoBehaviour, IColorable
{
    [SerializeField] private GameObject _liquid;

    private Color _mainColor;
    private VesselAggregator _aggregator;

    public Color Color => _mainColor;

    public event Action Completed;

    private void Awake()
    {
        _aggregator = GetComponent<VesselAggregator>();
    }

    public void TakeMagic(MagicCell cell)
    {
        //сделать проверку на соответсвие цвета

        if (_aggregator.IsFull)
            Completed?.Invoke();

        _aggregator.GrowUpVolume();
    }

    public void Init(Color color)
    {
        _mainColor = color;

        Renderer renderer = _liquid.GetComponent<Renderer>();
        renderer.material.color = _mainColor;
    }

    public void SetColor(Color color)
    {
        throw new NotImplementedException();
    }
}

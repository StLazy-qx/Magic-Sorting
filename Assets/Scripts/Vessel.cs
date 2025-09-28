using System;
using UnityEngine;

[RequireComponent(typeof(VolumeAggregator))]

public class Vessel : MonoBehaviour, IColorable
{
    [SerializeField] private Liquid _liquid;
    [SerializeField] private int _maxSize;
    [SerializeField] private int _points;

    private Color _mainColor;
    private VolumeAggregator _aggregator;

    public int Count => _maxSize;
    public bool IsActive => gameObject.activeSelf;
    public Color Color => _mainColor;
    public Liquid Liquid => _liquid;
    public bool IsFilled { get; private set; }

    public event Action<Vector3> Filled;
    //По другому назвать событие
    public event Action<Vector3, int, Color> ScoreUpdated;

    private void Awake()
    {
        _aggregator = GetComponent<VolumeAggregator>();
        _aggregator.InitParameters(_maxSize, _liquid);
        IsFilled = false;
    }

    public void TakeMagic(MagicCell cell)
    {
        if (cell == null)
            return;

        _aggregator.GrowUpVolume();

        if (_aggregator.IsFull)
        {
            IsFilled = true;

            ScoreUpdated?.Invoke(transform.position, _points, _mainColor);
            Filled?.Invoke(transform.position);
            gameObject.SetActive(false);
        }
    }

    public void SetColor(Color color)
        => _mainColor = color;
}

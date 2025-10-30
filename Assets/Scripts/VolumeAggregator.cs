using UnityEngine;
using DG.Tweening;
using System;

[RequireComponent(typeof(Vessel))]

public class VolumeAggregator : MonoBehaviour
{
    [SerializeField] private Transform _internalVolume;

    private int _vesselVolume;
    private int _currentSize = 0;
    private float _deltaSize;
    private Vector3 _initialBottomPoint;
    private Liquid _liquid;

    public bool IsFull => _currentSize >= _vesselVolume;

    public event Action<int> SizeChanged;

    //добавить в сосуд
    public void InitParameters(int vesselVolume, Liquid liquid)
    {
        _vesselVolume = vesselVolume > 0 ? vesselVolume :
            throw new ArgumentException("Объем сосуда должен быть больше 0", nameof(vesselVolume));

        _liquid = liquid ?? 
            throw new ArgumentNullException(nameof(liquid), "Жидкость не может быть null");

        if (_internalVolume == null)
            throw new InvalidOperationException("Internal Volume не назначен в инспекторе");

        _deltaSize = _internalVolume.localScale.y / _vesselVolume;

        SetupInitialLiquidPosition();
    }

    public void GrowUpVolume()
    {
        _currentSize++;
        SizeChanged?.Invoke(_currentSize);

        if (_liquid.gameObject.activeSelf == false)
            _liquid.gameObject.SetActive(true);

        UpdateLiquidVisual();
    }

    private void UpdateLiquidVisual()
    {
        float newHeight = _deltaSize * _currentSize;

        _liquid.transform
            .DOScaleY(newHeight, 0.3f)
            .SetEase(Ease.OutQuad);

        float yOffset = newHeight / 2f;
        Vector3 newPosition = _initialBottomPoint + new Vector3(0, yOffset, 0);

        _liquid.transform
            .DOMoveY(newPosition.y, 0.3f)
            .SetEase(Ease.OutQuad);
    }

    private void SetupInitialLiquidPosition()
    {
        float halfHeight = _internalVolume.localScale.y / 2f;
        _initialBottomPoint = _internalVolume.position - new Vector3(0, halfHeight, 0);

        _liquid.transform.localScale = new Vector3(
            _internalVolume.localScale.x,
            0f,
            _internalVolume.localScale.z
        );

        float yOffset = _deltaSize / 2f;
        _liquid.transform.position = _initialBottomPoint + new Vector3(0, yOffset, 0);
    }
}

using UnityEngine;
using DG.Tweening;
using System;

[RequireComponent(typeof(Vessel))]

public class VolumeAggregator : MonoBehaviour
{
    [SerializeField] private Transform _internalVolume;
    //[SerializeField] private Liquid _liquid;

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
        //тернарный оператор с исключением
        _vesselVolume = vesselVolume;
        _liquid = liquid;

        // перенести в отдельный метод
        _deltaSize = _internalVolume.localScale.y / _vesselVolume;

        Vector3 center = _internalVolume.position;
        float halfHeight = _internalVolume.localScale.y / 2f;
        _initialBottomPoint = center - new Vector3(0, halfHeight, 0);

        Vector3 initialScale = new Vector3(
            _internalVolume.localScale.x,
            0f,
            _internalVolume.localScale.z
        );

        _liquid.transform.localScale = initialScale;

        float yOffset = _deltaSize / 2f;
        Vector3 startPosition = _initialBottomPoint + new Vector3(0, yOffset, 0);
        _liquid.transform.position = startPosition;
    }

    public void GrowUpVolume()
    {
        _currentSize++;
        SizeChanged?.Invoke(_currentSize);

        if (_liquid.gameObject.activeSelf == false)
            _liquid.gameObject.SetActive(true);

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
}

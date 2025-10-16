using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ColumnColorDistributor : MonoBehaviour
{
    [SerializeField] private ParticlePool _particlePool;

    private IReadOnlyList<Vessel> _vessels;
    private List<Color> _colors = new List<Color>();
    private Queue<Color> _mixedColors = new Queue<Color>();

    public bool IsInitialized { get; private set; }
    public int TotalColors => _colors.Count;

    public void Initialize(IReadOnlyList<Vessel> vessels)
    {
        ValidateVessels(vessels);

        _vessels = vessels;

        GenerateColorList();
        ShuffleColors();

        IsInitialized = true;
    }

    public bool TryGetRandomColor(out Color color)
    {
        color = Color.white;

        if (IsInitialized == false)
            return false;

        return _mixedColors.TryDequeue(out color);
    }

    public void Reset()
    {
        ValidateVessels(_vessels);
        GenerateColorList();
        ShuffleColors();

        IsInitialized = true;
    }

    private void ValidateVessels(IReadOnlyList<Vessel> vessels)
    {
        if (vessels == null)
            throw new ArgumentNullException(nameof(vessels), "Список сосудов должен быть инифиализирован");

        if (vessels.Count == 0)
            throw new ArgumentException("Список сосудов не может быть пустым", nameof(vessels));

        if(vessels.Any(vessel => vessel == null))
            throw new ArgumentException("Список сосудов содержит нулевой элемент", nameof(vessels));
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

        // пока метод здесь для проверки но изменить место
        _particlePool.Initialize(TotalColors);
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
            _mixedColors.Enqueue(color);
    }
}

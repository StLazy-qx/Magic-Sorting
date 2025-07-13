using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]

public class MagicCell : MonoBehaviour, IColorable
{
    [SerializeField] private ColorRandomizer _colorRandomizer;

    private Renderer _renderer;

    public Color Color => _renderer.material.color;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();

        SetColor();
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    public void SetColor()
    {
        _renderer.material.color = _colorRandomizer.GenerateRandomColor();
    }

    public void SetColor(Color color)
    {
        throw new System.NotImplementedException();
    }
}

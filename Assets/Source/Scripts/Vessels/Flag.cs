using UnityEngine;

[RequireComponent(typeof(Renderer))]

public class Flag : MonoBehaviour, IColorable
{
    private Renderer _renderer;

    public Color Color => _renderer.material.color;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void SetColor(Color color)
    {
        _renderer.material.color = color;
    }
}

using UnityEngine;

[RequireComponent(typeof(Renderer))]

public class Liquid : MonoBehaviour, IColorable
{
    private Renderer _renderer;

    public Color Color => _renderer.material.color;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void SetColor(Color color)
    {
        // проверка

        _renderer.material.color = color;

        gameObject.SetActive(false);
    }

    public void Activated()
    {
        gameObject.SetActive(true);
    }
}

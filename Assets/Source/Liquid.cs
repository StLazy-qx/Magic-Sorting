using UnityEngine;

[RequireComponent(typeof(Renderer))]

public class Liquid : MonoBehaviour 
{
    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void SetColor(Color color)
    {
        // проверка

        _renderer.material.color = color;
    }
}

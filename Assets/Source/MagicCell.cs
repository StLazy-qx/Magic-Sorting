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
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    //public void SetRandomColor()
    //{
    //    _renderer.material.color = _colorRandomizer.GenerateRandomColor();
    //}

    public void SetColor(Color color)
    {
        _renderer.material.color = color;
    }
}

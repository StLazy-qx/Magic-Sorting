using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private int _price;
    [SerializeField] private Texture _texture;

    public int Price => _price;
    public Texture Texture => _texture;
    public bool IsBuyed { get; private set; }

    private void Awake()
    {
        IsBuyed = false;
    }

    public void Buy()
    {
        IsBuyed = true;
    }
}

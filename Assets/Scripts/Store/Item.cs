using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private int _price;
    [SerializeField] private Texture _texture;

    public int Price => _price;
    public Texture Texture => _texture;
}

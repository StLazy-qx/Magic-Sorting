using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemView _itemView;

    private Texture _texture;
    private int _price;

    public int Price => _price;
    public Texture Texture => _texture;
    public ItemView View => _itemView;
    public bool IsBuyed { get; private set; }

    private void Awake()
    {
        IsBuyed = false;
    }

    public void Initialize(ItemSO itemData)
    {
        if (itemData == null && _itemView == null)
            return;

        _itemView.Initialize(itemData);

        _price = itemData.Price;
        _texture = itemData.Scin;
    }

    public void ChangeBackgroundColor(Color color)
    {
        _itemView.ChangeBackgroundColor(color);
    }

    public void Buy()
    {
        IsBuyed = true;
    }
}

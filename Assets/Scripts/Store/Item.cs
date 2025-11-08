using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemView _itemView;

    private string _id;
    private Texture _texture;
    private int _price;

    public string ID => _id;
    public int Price => _price;
    public Texture Texture => _texture;
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

    public void Buy()
    {
        IsBuyed = true;
    }
}

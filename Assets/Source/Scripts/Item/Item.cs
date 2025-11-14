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
    public ItemView View => _itemView;
    public bool IsBuyed { get; private set; }


    public void Initialize(ItemSO itemData)
    {
        if (itemData == null && _itemView == null)
            return;

        _itemView.Initialize(itemData);

        _id = itemData.ID;
        _price = itemData.Price;
        _texture = itemData.Scin;
    }

    public void Buy()
    {
        IsBuyed = true;

        _itemView.ActivateBoughtText();
    }

    public void ActivateBought()
    {
        _itemView.ActivateBoughtText();
    }
}
